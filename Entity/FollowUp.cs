using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FollowUp : BaseEntity
    {
        public string Name { get; set; }
        public string PhysicalExamination { get; set; }
        public string LaboratoryTests { get; set; }
    }
}
