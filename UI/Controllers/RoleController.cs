using BusinessLogicLayer.Abstracts;
using BusinessLogicLayer.FluentValidationRules.Role;
using Core.ResultType;
using DataTransferObject.ModuleRole;
using DataTransferObject.Role;
using DataTransferObject.User;
using Microsoft.AspNetCore.Mvc;
using MSC.Extentions.Filters;
using UI.Filters;
using UI.Models;

namespace UI.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IModuleBL _moduleBL;
        private readonly IRoleBL _roleBL;
        private readonly IModuleRoleBL _moduleRoleBL;
        private readonly IUserBL _userBL;

        public RoleController(IModuleBL moduleBL, IRoleBL roleBL, IModuleRoleBL moduleRoleBL, IUserBL userBL)
        {
            _userBL = userBL;
            _moduleBL = moduleBL;
            _roleBL = roleBL;
            _moduleRoleBL = moduleRoleBL;
        }

        [AuthorizeFilter]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeFilter]
        public IActionResult GetList()
        {
            var res = _roleBL.GetAll();
            return Ok(res);
        }

        [AuthorizeFilter]
        public IActionResult GetById([FromQuery] RequestModel model)
        {
            Result<RoleDTO> res = _roleBL.GetById(model.Id);
            return Ok(res);
        }

        //[AuthorizeFilter]
        [HttpGet]
        public IActionResult RolAuthentication(int id)
        {
            var moduleList = _moduleBL.GetModuleListWithSubs();
            var role = _roleBL.GetById(id);
            Result<List<ModuleRoleDTO>> authList = _moduleRoleBL.GetAuthorizedModuleList(id).GetAwaiter().GetResult();

            ModuleRoleModel model = new ModuleRoleModel
            {
                RoleId = id,
                AuthModuleList = authList.Data,
                ModuleList = moduleList.Data
            };
            return View(model);
        }

        [AuthorizeFilter]
        [HttpPost]
        public IActionResult RolAuthentication(int[] moduleList, int id)
        {
            int userId = GetUserId();
            _roleBL.AuthorizeRole(moduleList, id,userId);
            return RedirectToAction("Index");
        }

        [AuthorizeFilter]
        [HttpGet]
        public IActionResult Create()
        {
            //RoleDTO roleDTO = new RoleDTO();
            return View();
        }

        [AuthorizeFilter]
        [HttpPost]
        [ValidationFilter(typeof(AddRoleRules))]
        public IActionResult Create([FromBody] RoleDTO role)
        {
            //throw new Exception("Hata aldık");
            var res = _roleBL.Add(role);
            return Ok(res);
        }

        [AuthorizeFilter]
        [HttpGet]
        public IActionResult Edit([FromQuery] RequestModel model)
        {
            return View(model);
        }


        [AuthorizeFilter]
        [HttpPost]
        public IActionResult Edit([FromBody] RoleDTO roleDTO)
        {
            Result<RoleDTO> res = _roleBL.Update(roleDTO);
            return Ok(res);
           
        }

        [AuthorizeFilter]
        [HttpGet]
        public IActionResult Delete([FromQuery] RequestModel model)
        {
            Result<RoleDTO> res = _roleBL.Delete(model.Id);
            return Ok(res);
        }
    }
}
