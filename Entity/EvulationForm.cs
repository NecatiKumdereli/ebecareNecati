using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EvulationForm : BaseEntity
    {
        public int UserId { get; set; }
        public string Design { get; set; }
        public string Intelligibility { get; set; }
        public string IsBenefit { get; set; }
        public string Benefits { get; set; }
        public string Rating { get; set; }
        public string IsDifficulty { get; set; }
        public string Difficults { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User User { get; set; }
    }
}
