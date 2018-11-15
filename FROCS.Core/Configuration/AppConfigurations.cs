using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Core.Configuration
{
    public static class AppConfigurations
    {
        public static readonly string AppId = "7Xc17SXehxuiTroEH5E9Rk1SM16Fjj1CdeN73cm2ASJg";  // APP_ID
        public static readonly string FtKey = "4n7Noj2u7AJHyuYXuvmwWLKH4Jy1vfyqsEi2nZL7S6TF";  // 人脸追踪 key
        public static readonly string FdKey = "4n7Noj2u7AJHyuYXuvmwWLKQDiEAykvMDXgSqesJDp2U";  // 人脸检测 key
        public static readonly string FrKey = "4n7Noj2u7AJHyuYXuvmwWLKtsKGqiATnyCyL1SUsaD5M";  // 人脸识别 key
        public static readonly string AgeKey = "4n7Noj2u7AJHyuYXuvmwWLL9C7oAsME7jodCvrt3iBS1"; // 年龄识别 key
        public static readonly string GenderKey = "4n7Noj2u7AJHyuYXuvmwWLLGMX4LA6Tfdggd6o1cKGPH"; // 性别识别 key
        public static readonly string FaceImagesPath = "faceimages";  // 人脸截图保存路径
    }
}
