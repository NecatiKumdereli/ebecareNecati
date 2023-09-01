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
    public class ModuleRepository : EfRepositoryBase<Module, BaseDbContext>, IModuleRepository
    {
        public ModuleRepository(BaseDbContext context) : base(context)
        {
        }

        public void CustomDelete(Module module)
        {
            using (BaseDbContext mycontext = new())
            {
                mycontext.Entry<Module>(module).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                int count = mycontext.SaveChanges();
            }
        }
    }
}
