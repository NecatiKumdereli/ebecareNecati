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
        public string name { get; set; }
        public DateTime date { get; set; }



    }
}
