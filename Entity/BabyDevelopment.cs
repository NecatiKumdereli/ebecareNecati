using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BabyDevelopment : BaseEntity
    {
        public string Name { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Heigth { get; set; }

        public int Weight { get; set; }






    }
}
