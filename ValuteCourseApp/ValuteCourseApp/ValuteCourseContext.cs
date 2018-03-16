using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ValuteCourseApp
{
    class ValuteCourseContext : DbContext 
    {
        public ValuteCourseContext()
            : base("CBRINFEntities")
        { }
          
        public DbSet<ValuteCourse> ValuteCourse { get; set; }  //содержит объекты таблицы в виде класса ValuteCourse
    }
}
