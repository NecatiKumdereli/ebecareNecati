using BusinessLogicLayer.Abstracts;
using Core.ResultType;
using DataTransferObject.Module;
using Microsoft.AspNetCore.Mvc;
using MSC.Extentions.Filters;
using UI.Models;

namespace UI.Controllers
{
    public class ModuleController : BaseController
    {
        private readonly IModuleBL _moduleBL;

        public ModuleController(IModuleBL moduleBL)
        {
            _moduleBL = moduleBL;
        }

        [AuthorizeFilter]
        public IActionResult Index([FromQuery] RequestModel requestModel)
        {
            return View(requestModel);
        }

        [AuthorizeFilter]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] RequestModel requestModel)
        {
            var res = await _moduleBL.GetAll(requestModel.Page, requestModel.Id);
            return Ok(res);
        }

        [AuthorizeFilter]
        [HttpGet]
        public IActionResult GetById([FromQuery] RequestModel model)
        {
            Result<ModuleDTO> res = _moduleBL.GetById(model.Id);
            return Ok(res);
        }

        [AuthorizeFilter]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [AuthorizeFilter]
        [HttpPost]
        public IActionResult Create([FromBody] ModuleDTO moduleDTO)
        {
            Result<ModuleDTO> res = _moduleBL.Add(moduleDTO);
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
        public IActionResult Edit([FromBody] ModuleDTO moduleDTO)
        {
            var res = _moduleBL.Update(moduleDTO);
            return Ok(res);
        }

        [AuthorizeFilter]
        [HttpGet]
        public IActionResult Delete([FromQuery] RequestModel model)
        {
            //result<moduledto> module = _modulebl.getbyıd(model.ıd);
            //if (!module.ıssuccess)
            //{
            //    return ok(module);
            //}
            Result<ModuleDTO> res = _moduleBL.Delete(model.Id);
            return Ok(res);
        }
    }
}
