using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.PregnancyVaccine
{
    public class PregnancyVaccineDTO
    {
        public string VaccineName { get; set; }
        public string Description { get; set; }
        public int RecommendedWeek { get; set; }
        public string DosageInformation { get; set; }
        public int NurseName { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
