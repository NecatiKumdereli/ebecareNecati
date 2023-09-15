using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class MenstrualDate : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}
