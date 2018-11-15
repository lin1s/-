using Stepon.FaceRecognization.Recognization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Application.Dtos
{
    public class PersonFaceFeature
    {
        public int Id { get; set; }       
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public byte[] FaceFeature { get; set; }
        public string Description { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
