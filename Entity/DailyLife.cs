using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class DailyLife : BaseEntity
    {
        public int WeekOfPregnancy { get; set; } 
        public string Topic { get; set; } 
        public string Description { get; set; }
        public List<string> Tips { get; set; } 
        public DateTime DateCreated { get; set; } 



    }
}
