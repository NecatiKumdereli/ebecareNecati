using DataTransferObject.Module;
using DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.ModuleRole
{
    public class ModuleRoleDTO
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }

        public RoleDTO RoleDTO { get; set; }
        public ModuleDTO ModuleDTO { get; set; }
    }
}
