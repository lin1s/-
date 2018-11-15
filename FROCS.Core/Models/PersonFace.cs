using Stepon.FaceRecognization.Recognization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Core.Models
{
    [Table("AppPersonFace")]
    public class PersonFace
    {
        private const int MaxSerialNumberLength = 20;
        private const int MaxNameLength = 128;
        private const int MaxPositionLength = 128;
        private const int MaxDescriptionLength = 64 * 10;
        private const int MaxFaceFeatureLength = 1024 * 23;
        private const int MaxUrlLength = 1024 * 4;
        [Key]
        public int Id { get; set; }
        [StringLength(MaxSerialNumberLength)]
        public string SerialNumber { get; set; }
        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }
        [StringLength(MaxPositionLength)]
        public string Position { get; set; }
        [StringLength(MaxUrlLength)]
        public string ImageUrl { get; set; }
        [Required]        
        public byte[] FaceFeature { get; set; }
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }
        public DateTime? CreationTime { get; set; }
        public PersonFace()
        {
            CreationTime = DateTime.Now;
        }

    }
}
