using FROCS.Application.Dtos;
using FROCS.Core;
using FROCS.Core.Models;
using FROCS.EntityFramework.Interface;
using FROCS.EntityFramework.Repository;
using Stepon.FaceRecognization.Recognization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.Application
{
    public class PersonFaceService : IDisposable
    {
        PersonFaceRepository _personFaceResitory = new PersonFaceRepository();

        public List<PersonFace> GetPersonFaceList()
        {
            var faces = _personFaceResitory.GetAllPersonFaces().ToList();
            return faces;
        }

        public PersonFace GetPersonFaceById(int id)
        {
            var face = _personFaceResitory.FindPersonFaceById(id);
            return face;
        }

        public PersonFace InsertPersionFace(PersonFace personFace)
        {
            //PersonFace face = new PersonFace();
            //face.Id = personFace.Id;
            //face.SerialNumber = personFace.SerialNumber;
            //face.Name = personFace.Name;
            //face.ImageUrl = personFace.ImageUrl;
            //face.Position = personFace.Position;
            //face.Description = personFace.Description;
            //face.CreationTime = personFace.CreationTime;
            //face.FaceFeature = personFace.FaceFeature.Data;
            personFace.CreationTime = DateTime.Now;
            return _personFaceResitory.InsertPersionFace(personFace);
        }

        public PersonFace UpdatePersonFace(PersonFace personFace)
        {
            return _personFaceResitory.UpdatePersonFace(personFace);
        }


        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        private PersonFaceFeature FaceToPersonFaceFeature(Core.Models.PersonFace face)
        {
            var personFace = new PersonFaceFeature();
            personFace.Id = face.Id;
            personFace.SerialNumber = face.SerialNumber;
            personFace.Name = face.Name;
            personFace.ImageUrl = face.ImageUrl;
            personFace.Position = face.Position;
            personFace.Description = face.Description;
            personFace.CreationTime = face.CreationTime;
            personFace.FaceFeature = new FaceModel(face.FaceFeature);
            return personFace;
        }

        public void Dispose()
        {
            //_personFaceResitory.Dispose();
        }
    }
}
