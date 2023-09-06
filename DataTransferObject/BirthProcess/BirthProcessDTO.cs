using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.BirthProcess
{
    public class BirthProcessDTO
    {
        public string ProcessName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfProcess { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
    }
}
