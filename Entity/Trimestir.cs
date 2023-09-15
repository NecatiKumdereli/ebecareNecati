using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Trimestir : BaseEntity
    {
        public int Text { get; set; }
        public int TrimestirType { get; set; }
        public int CreateDate { get; set; }
        public int UpdateDate { get; set; }
        public HashSet<BabyDevelopment> BabyDevelopments { get; set; }
        public HashSet<ChangesBody> ChangesBodies { get; set; }
    }
}
