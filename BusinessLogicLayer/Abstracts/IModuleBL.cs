using Core.Paging;
using Core.ResultType;
using DataTransferObject.Module;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstracts
{
    public interface IModuleBL
    {
        Result<ModuleDTO> Add(ModuleDTO model);
        Result<ModuleDTO> Update(ModuleDTO model);
        Result<ModuleDTO> Delete(int id);

        Result<ModuleDTO> GetById(int id);
        Result<List<ModuleDTO>> GetModuleListWithSubs();
        Task<Result<IPaginate<Module>>> GetAll(int page,int id = 0);
    }
}
