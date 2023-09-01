using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.Module
{
    public class ModuleDTO
    {
        public ModuleDTO()
        {
            SubModules = new List<ModuleDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public int Menu { get; set; }
        public int ParentId { get; set; }

        public List<ModuleDTO> SubModules { get; set; }
    }
}
