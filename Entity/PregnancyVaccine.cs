using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PregnancyVaccine : BaseEntity
    {
        public string VaccineName { get; set; } 
        public string Description { get; set; } 
        public int RecommendedWeek { get; set; }
        public string DosageInformation { get; set; }
        public int NurseName { get; set; }
        public DateTime DateUpdated { get; set; } 

    }
}
