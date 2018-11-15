using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROCS.EntityFramework.Data
{
    public class DefaultContext : DbContext
    {
        public DbSet<FROCS.Core.Models.PersonFace> PersonFaces { get; set; }

        public DefaultContext() : base("name=DefaultConnectionString")
        {
        }
    }
}
