using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RiskAssessmentUser : BaseEntity
    {
        public int UserId { get; set; }
        public int RiskAssessmentFormId { get; set; }
        public bool Answer { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public RiskAssessmentForm RiskAssessmentForm { get; set; }
        public User User { get; set; }
    }
}
