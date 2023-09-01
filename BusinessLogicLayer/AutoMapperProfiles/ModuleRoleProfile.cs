using AutoMapper;
using DataTransferObject.ModuleRole;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.AutoMapperProfiles
{
    public class ModuleRoleProfile : Profile
    {
        public ModuleRoleProfile()
        {
            CreateMap<ModuleRoleDTO, ModuleRoles>().ReverseMap();
        }
    }
}
