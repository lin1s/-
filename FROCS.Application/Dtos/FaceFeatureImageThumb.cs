using Stepon.FaceRecognization.Recognization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Application.Dtos
{
    public class FaceFeatureImageThumb
    {
        public string ImageFileName { get; set; }
        public Feature Feature { get; set; }
        public Bitmap Image { get; set; }
        public Bitmap Thumbnail { get; set; }
    }
}
