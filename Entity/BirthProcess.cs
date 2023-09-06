using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BirthProcess : BaseEntity
    {
        public string ProcessName { get; set; } 
        public string Description { get; set; } 
        public DateTime DateOfProcess { get; set; } 
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
    }
}
