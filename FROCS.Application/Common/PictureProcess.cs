using Stepon.FaceRecognization.Recognization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Application.Common
{
    public static class PictureProcess
    {
        /// <summary>
        /// 截图人脸，生成150*150的缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static Bitmap GetThumbnail(Bitmap image, Feature feature)
        {
            int thumbWidth = 150;
            int thumbHeight = 150;

            // 截取的图像在人脸识别区的基础上扩大20%
            var faceWidth = feature.FaceLoaction.Width;      // 获取 人脸的宽度
            var percent = Convert.ToInt32(faceWidth * 0.2);  // 宽度的 20%
            var faceX = feature.FaceLoaction.X - percent;    // X坐标 左移
            faceX = faceX < 0 ? 0 : faceX;
            var faceY = feature.FaceLoaction.Y - percent;    // Y坐标 上移
            faceY = faceY < 0 ? 0 : faceY;
            faceWidth = faceWidth + percent * 2;             // 宽度 增加
            faceWidth = faceWidth > image.Width ? image.Width : faceWidth;
            var faceHeight = faceWidth;                      // 高度与宽度相同
            faceHeight = faceHeight > image.Height ? image.Height : faceHeight;

            var faceImage = image.Clone(new Rectangle(faceX, faceY, faceWidth, faceHeight),
                                image.PixelFormat);

            Bitmap img = new Bitmap(thumbWidth, thumbHeight, PixelFormat.Format32bppRgb);
            if (thumbWidth >= faceImage.Width)
            {
                img = faceImage;
            }
            else
            {
                // 如果人脸载图大于 150*150，生成新的缩略图

                img.SetResolution(faceImage.HorizontalResolution, faceImage.VerticalResolution);
                using (var g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(faceImage, new Rectangle(0, 0, thumbWidth, thumbHeight),
                        new Rectangle(0, 0, faceImage.Width, faceImage.Height), GraphicsUnit.Pixel);


                }
            }

            faceImage.Dispose();

            return img;

            // return faceImage;
        }
    }
}
