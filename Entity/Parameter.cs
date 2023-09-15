using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Parameter : BaseEntity
    {
        public int ParameterType { get; set; }
        public string Test { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public HashSet<DailyLife> DailyLives { get; set; }
        public HashSet<BirthProcess> BirthProcesses { get; set; }
        public HashSet<PospartumProcess> PospartumProcesss { get; set; }
    }
}
