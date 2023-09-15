using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Communication : BaseEntity
    {
        public int UserId { get; set; }
        public bool Status { get; set; }
        public int Situation { get; set; }
        public int SentUser { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User User { get; set; }
        
    }
}
