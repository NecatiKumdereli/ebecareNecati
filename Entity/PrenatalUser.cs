using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PrenatalUser : BaseEntity
    {
        public int PrenatalFormId { get; set; }
        public int UserId { get; set; }
        public string Answer { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User User { get; set; }
        public PrenatalForm PrenatalForm { get; set; }
    }
}
