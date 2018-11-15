using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using Stepon.FaceRecognization.Extensions;
using System;
using System.Net.Mime;
using FROCS.Core.Models;
using FROCS.Core;
using FROCS.Application.Dtos;
using Stepon.FaceRecognization;
using Stepon.FaceRecognization.Age;
using Stepon.FaceRecognization.Gender;
using Stepon.FaceRecognization.Common;
using Stepon.FaceRecognization.Detection;
using Stepon.FaceRecognization.Tracking;
using Stepon.FaceRecognization.Recognization;
using System.Collections.Generic;
using System.Linq;
using FROCS.Core.Configuration;
using FROCS.EntityFramework.Repository;
using OpenCvSharp;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FROCS.Application
{
    /// <summary>
    /// 用于静态图片文件的人脸检测服务
    /// </summary>
    public class FaceDetectionService : IDisposable
    {
        private FaceAge _age;              // 用于检测年龄
        private FaceGender _gender;        // 用于检测性别
        private FaceDetection _detection;  // 用于静态图片抽取特征
        private FaceRecognize _recognize;  // 用于人脸识别
        private FaceProcessor _processor;  // 面部识别的综合处理器，主要用于检测加特征抽取
        private FaceTracking _traking;     // 用于视频流检测人脸

        // PersonFaceService _personFaceService;
        PersonFaceRepository _personFaceRepository;

        

        public FaceDetectionService()
        {
            _age = new FaceAge(AppConfigurations.AppId, AppConfigurations.AgeKey);           // 年龄识别
            _gender = new FaceGender(AppConfigurations.AppId, AppConfigurations.GenderKey);  // 性别识别
            //// 图片检测人脸
            _detection = LocatorFactory.GetDetectionLocator(AppConfigurations.AppId, AppConfigurations.FdKey, _age, _gender) as FaceDetection;
            _traking = LocatorFactory.GetTrackingLocator(AppConfigurations.AppId, AppConfigurations.FtKey, _age, _gender) as FaceTracking;

            _recognize = new FaceRecognize(AppConfigurations.AppId, AppConfigurations.FrKey);

            _processor = new FaceProcessor(_detection, _recognize);
            _personFaceRepository = new PersonFaceRepository();
        }



        /// <summary>
        /// 根据locate数据，从图片中提供人脸特征，先执行 FaceLocateResult() 获得图片的人脸特征
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="locate">图片中的人脸数据</param>
        /// <returns>返回一个人脸特征，适用于图片中只有一个人脸的情况</returns>
        public Feature GetFaceFeature(Bitmap bitmap, LocateResult locate)
        {
            Feature feature = null;

            var result = _detection.Detect(bitmap, out var locateResult);
            if (result == Stepon.FaceRecognization.Common.ErrorCode.Ok && locateResult.HasFace)
            {
                using (_recognize = new FaceRecognize(AppConfigurations.AppId, AppConfigurations.FrKey))
                {
                    feature = _recognize.ExtractFeature(locate.OffInput, locate.Faces[0], locate.FacesOrient[0]);
                }
            }

            return feature;
        }



        /// <summary>
        /// 返回图片人脸数据
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public LocateResult FaceLocateResult(Bitmap bitmap)
        {
            LocateResult _locateResult = null;

            var code = _detection.Detect(bitmap, out var locateResult);
            try
            {
                if (code == Stepon.FaceRecognization.Common.ErrorCode.Ok)
                {
                    _locateResult = locateResult;

                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

            return _locateResult;
        }

        /// <summary>
        /// 返回所有相似度 0.5 以上的人脸，并按相似度倒排序
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public List<FindPersonFaceResult> FindPersonFaceByFeature(Feature feature)
        {
            var _cache = _personFaceRepository.GetAllPersonFaces();
            if (_cache.Count == 0)
            {
                return null;
            }
            List<FindPersonFaceResult> resultList = new List<FindPersonFaceResult>();

            foreach (var single in _cache)
            {
                var sim = _processor.Match(feature.FeatureData, single.FaceFeature);
                if (sim > 0.5)
                {
                    FindPersonFaceResult result = new FindPersonFaceResult();
                    result.PersonFace = single;
                    result.Similar = sim;
                    resultList.Add(result);
                }
            }

            return resultList
                .OrderByDescending(r => r.Similar)
                .ToList();
        }

        /// <summary>
        /// 返回所有相似度 0.5 以上的人脸，并按相似度倒排序
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        public List<FindPersonFaceResult> FindPersonFaceByFeature(Feature feature, List<PersonFace> cache)
        {
            //var _cache = _personFaceRepository.GetAllPersonFaces();
            if (cache.Count == 0)
            {
                return null;
            }
            List<FindPersonFaceResult> resultList = new List<FindPersonFaceResult>();

            foreach (var single in cache)
            {
                var sim = _processor.Match(feature.FeatureData, single.FaceFeature);
                if (sim > 0.5)
                {
                    FindPersonFaceResult result = new FindPersonFaceResult();
                    result.PersonFace = single;
                    result.Similar = sim;
                    resultList.Add(result);
                }
            }

            return resultList
                .OrderByDescending(r => r.Similar)
                .ToList();
        }

        /// <summary>
        /// 查找相似度0.5以上，返回相似度最高的一个人脸
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public FindPersonFaceResult FindPersonFaceByFeatureFirstOrDefault(Feature feature)
        {
            return FindPersonFaceByFeature(feature).FirstOrDefault();
        }

        /// <summary>
        /// 查找相似度0.5以上，返回相似度最高的一个人脸
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        public FindPersonFaceResult FindPersonFaceByFeatureFirstOrDefault(Feature feature, List<PersonFace> cache)
        {
            return FindPersonFaceByFeature(feature, cache).FirstOrDefault();
        }


        public void Dispose()
        {
            if (_processor != null)
            {
                _processor.Dispose();
            }

            //if (_recognize != null)
            //{
            //    _recognize.Dispose();
            //}
            //if (_detection != null)
            //{
            //    _detection.Dispose();
            //}
            //if (_age != null)
            //{
            //    _age.Dispose();
            //}
            //if (_gender != null)
            //{
            //    _gender.Dispose();
            //}

            //  throw new NotImplementedException();
        }


    }
}
