using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RiskAssessmentForm : BaseEntity
    {
        public string Question { get; set; }
        public int TypeQuestion { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public HashSet<RiskAssessmentUser> RiskAssessmentUsers { get; set; }
    }
}
