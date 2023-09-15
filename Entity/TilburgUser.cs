using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TilburgUser : BaseEntity
    {
        public int UserId { get; set; }
        public int TilburgFormId { get; set; }
        public int Answer { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public TilburgForm TilburgForm { get; set; }
        public User User { get; set; }
        
    }
}
