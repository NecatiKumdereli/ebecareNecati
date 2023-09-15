using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FrequencyOfUsing : BaseEntity
    {

        public int UserId { get; set; }
        public int FrequencyDay { get; set; }
        public int Danger { get; set; }
        public int Complaint { get; set; }
        public int ContractionRecords { get; set; }
        public int FetalCounter { get; set; }
        public int FollowUp { get; set; }
        public int TestVaccine { get; set; }
        public int ChangesPregnancy { get; set; }
        public int DailyLife { get; set; }
        public int BirthProcess { get; set; }
        public int PospartumProcess { get; set; }
        public int Menu { get; set; }
        public int Communication { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User User { get; set; }
    }
}
