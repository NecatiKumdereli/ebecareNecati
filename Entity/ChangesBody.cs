using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ChangesBody : BaseEntity
    {
        public string Menstruation { get; set; }
        public int WeekOfPregnancy { get; set; } 
        public string Description { get; set; }

    }
}
