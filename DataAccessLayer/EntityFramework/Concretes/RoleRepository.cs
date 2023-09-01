using Core.DataAccess.Repositories;
using DataAccessLayer.EntityFramework.Abstracts;
using DataAccessLayer.EntityFramework.Context;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework.Concretes
{
    public class RoleRepository : EfRepositoryBase<Role, BaseDbContext>, IRoleRepository
    {
        public RoleRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
