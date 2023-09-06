using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class IndividualCare : BaseEntity
    {
      
        public string Topic { get; set; } 
        public string Description { get; set; } 
        public List<string> Tips { get; set; } 
        public DateTime DateCreated { get; set; } 
    }
}
