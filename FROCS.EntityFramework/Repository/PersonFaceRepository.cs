using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FROCS.Core.Models;
using FROCS.EntityFramework.Data;
using FROCS.EntityFramework.Interface;

namespace FROCS.EntityFramework.Repository
{
    public class PersonFaceRepository
    {
        private DefaultContext context = new DefaultContext();

        /// <summary>
        /// 获取所有的人脸
        /// </summary>
        /// <returns></returns>
        public List<PersonFace> GetAllPersonFaces()
        {
            return context.PersonFaces.ToList();
        }

        /// <summary>
        /// 添加一个人脸到人脸库
        /// </summary>
        /// <param name="face"></param>
        public PersonFace InsertPersionFace(PersonFace face)
        {
            PersonFace _face = null;
            try
            {
                 _face = context.PersonFaces.Add(face);
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            return _face;
        }

        /// <summary>
        /// 根据ID查找人脸
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PersonFace FindPersonFaceById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return context.PersonFaces.Find(id);
        }

        /// <summary>
        /// 更新人脸
        /// </summary>
        /// <param name="personFace"></param>
        /// <returns></returns>
        public PersonFace UpdatePersonFace(PersonFace personFace)
        {
            var _face = context.PersonFaces.Find(personFace.Id);
            try
            {
                

                _face.SerialNumber = personFace.SerialNumber;
                _face.Name = personFace.Name;
                _face.ImageUrl = personFace.ImageUrl;
                _face.Position = personFace.Position;
                _face.Description = personFace.Description;
                _face.CreationTime = personFace.CreationTime;
                _face.FaceFeature = personFace.FaceFeature;

                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            return _face;
        }

        public void Dispose()
        {
            context.Dispose();
        }


    }
}
