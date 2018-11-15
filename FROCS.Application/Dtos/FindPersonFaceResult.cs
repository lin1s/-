using FROCS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Application.Dtos
{
    public class FindPersonFaceResult
    {
        public PersonFace PersonFace { get; set; }

        public float Similar { get; set; }
    }
}
