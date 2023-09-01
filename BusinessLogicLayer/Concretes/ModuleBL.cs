using AutoMapper;
using BusinessLogicLayer.Abstracts;
using Core.Paging;
using Core.ResultType;
using DataAccessLayer.EntityFramework.Abstracts;
using DataTransferObject.Module;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concretes
{
    public class ModuleBL : IModuleBL
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModuleBL(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }

        public Result<ModuleDTO> Add(ModuleDTO model)
        {
            Result<ModuleDTO> result;

            Module module = _mapper.Map<Module>(model);

            _moduleRepository.Add(module);
            result = new Result<ModuleDTO>(true, model, "İşlem başarılı");
            return result;
        }

        public Result<ModuleDTO> Update(ModuleDTO model)
        {
            Result<ModuleDTO> result;

            Module module = _mapper.Map<Module>(model);
            _moduleRepository.Update(module);
            result = new Result<ModuleDTO>(true, model, "İşlem başarılı");
            return result;
        }

        public Result<ModuleDTO> Delete(int id)
        {
            Result<ModuleDTO> result;
            Module module = _moduleRepository.Get(w => w.Id == id);
            _moduleRepository.Delete(module);
            result = new Result<ModuleDTO>(true, "İşlem başarılı");
            return result;
        }

        public async Task<Result<IPaginate<Module>>> GetAll(int page,int id = 0)
        {
            Result<IPaginate<Module>> result;

            var moduleList = await _moduleRepository.GetListAsync(predicate: w => w.ParentId == id, index: page,size: 2, enableTracking: false);

            if (moduleList.Count <= 0)
            {
                result = new Result<IPaginate<Module>>(false, "Sistemde kayıtlı herhangi bir modül bulunamadı");
                return result;
            }
            result = new Result<IPaginate<Module>>(true, moduleList, "Modül listesi başarıya getirildi.");
            return result;
        }

        public Result<ModuleDTO> GetById(int id)
        {
            Result<ModuleDTO> result;

            Module module = _moduleRepository.Get(w => w.Id == id);
            if (module != null)
            {
                ModuleDTO moduleDTO = _mapper.Map<ModuleDTO>(module);
                result = new Result<ModuleDTO>(true, moduleDTO, "İşlem başarılı");
                return result;
            }
            else
            {
                result = new Result<ModuleDTO>(false, "Modül bulunamadı");
                return result;
            }
        }

        public Result<List<ModuleDTO>> GetModuleListWithSubs()
        {
            List<ModuleDTO> moduleList;
            Result<List<ModuleDTO>> result;
            List<ModuleDTO> subModules = new List<ModuleDTO>();

            List<Module> allModules = _moduleRepository.GetAsList().ToList();
            if (allModules == null && allModules.Count < 0)
            {
                result = new Result<List<ModuleDTO>>(false, "Sistemde kayıtlı modül bulunamadı");
                return result;
            }
            moduleList = _mapper.Map<List<ModuleDTO>>(allModules.Where(w => w.ParentId == 0));
            foreach (var item in moduleList)
            {
                var subModuleList = allModules.Where(w => w.ParentId == item.Id).ToList();
                subModules = _mapper.Map<List<ModuleDTO>>(subModuleList);
                foreach (var subModule in subModules)
                {
                    item.SubModules.Add(subModule);
                }
            }
            result = new Result<List<ModuleDTO>>(true, moduleList, "İşlem başarılı");
            return result;

        }
    }
}
