using FROCS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.EntityFramework.Interface
{
    public interface IPersonFaceRepository:IDisposable
    {
        List<PersonFace> GetPersonFaceList();
        PersonFace InsertPersionFace(PersonFace face);
        PersonFace FindPersonFaceById(int? id);
        PersonFace UpdatePersonFace(PersonFace personFace);


    }
}
