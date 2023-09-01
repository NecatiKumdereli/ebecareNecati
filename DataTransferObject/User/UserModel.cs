
using DataTransferObject.ModuleRole;
using DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.User
{
    public class UserModel
    {
        public UserModel()
        {
            AuthList = new List<ModuleRoleDTO>();
        }

        public int Id { get; set; }
        public int RolId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public RoleDTO roleDTO  { get; set; }

        public List<ModuleRoleDTO> AuthList { get; set; }
    }
}
