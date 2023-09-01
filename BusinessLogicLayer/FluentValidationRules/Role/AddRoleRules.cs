using DataTransferObject.Role;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.Role
{
    public class AddRoleRules : AbstractValidator<RoleDTO>
    {
        public AddRoleRules() 
        {
            RuleFor(r => r.Name).NotNull().NotEmpty().MinimumLength(5).WithName("Rol İsmi");
        }
    }
}
