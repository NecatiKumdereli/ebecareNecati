using Core.DataAccess.Repositories;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework.Abstracts
{
    public interface IModuleRepository : IAsyncRepository<Module>, IRepository<Module>
    {
        void CustomDelete(Module module);
    }
}
