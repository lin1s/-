using Stepon.FaceRecognization.Common;
using Stepon.FaceRecognization.Recognization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Application.Dtos
{
    public class FaceFeatureLocateResult
    {
        public Feature[] Features { get; set; }  // 人脸特征信息
        public LocateResult LocateResults { get; set; }  // 人脸的数量，尺寸等其他数据
    }
}
