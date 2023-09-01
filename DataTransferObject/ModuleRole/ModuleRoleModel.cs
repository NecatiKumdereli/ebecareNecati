using DataTransferObject.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.ModuleRole
{
    public class ModuleRoleModel
    {
        public ModuleRoleModel()
        {
            ModuleList = new List<ModuleDTO>();
            AuthModuleList = new List<ModuleRoleDTO>();
        }
        public int RoleId { get; set; }
        public List<ModuleDTO> ModuleList { get; set; }
        public List<ModuleRoleDTO> AuthModuleList { get; set; }
    }
}
