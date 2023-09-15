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
        public int ParameterId { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } 
        public DateTime UpdateDate { get; set; } 
        public Parameter Parameter { get; set; } 



    }
}
