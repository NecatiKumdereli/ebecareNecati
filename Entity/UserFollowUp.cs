using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserFollowUp : BaseEntity
    {
        public int UserId { get; set; }
        public int FollowUpId { get; set; }
        public string Story { get; set; }
        public string PhsicalExamination { get; set; }
        public string LabTest { get; set; }
        public string MedicationSupport { get; set; }
        public string Consultancy { get; set; }
        public string Education { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public FollowUp FollowUp { get; set; }
        public User User { get; set; }
    }
}
