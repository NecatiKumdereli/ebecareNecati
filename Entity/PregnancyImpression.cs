using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PregnancyImpression : BaseEntity
    {
        public int UserId { get; set; } 
        public int ImpressionId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; } 
        public User User { get; set; } 
        public Impression Impression { get; set; } 

    }
}
