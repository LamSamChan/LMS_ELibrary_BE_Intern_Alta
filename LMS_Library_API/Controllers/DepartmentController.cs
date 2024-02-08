﻿using LMS_Library_API.Models;
using LMS_Library_API.Services.DepartmentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentSvc _departmentSvc;
        public DepartmentController(IDepartmentSvc departmentSvc)
        {
            _departmentSvc = departmentSvc;
        }
        [HttpPost]
        public async Task<ActionResult> Create(Department department)
        {
            var listData = await _departmentSvc.GetAll();
            foreach (var item in listData)
            {
                if (department.Id == item.Id)
                {
                    return BadRequest("ID bộ môn đã tồn tại!");
                }
            }
            var loggerResult = await _departmentSvc.Create(department);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }

        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetAll()
        {
            var loggerResult = await _departmentSvc.GetAll();
            return loggerResult;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetById(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _departmentSvc.GetById(id);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(loggerResult);
                }
            }
            else
            {
                return BadRequest("Hãy điền ID để tìm kiếm đối tượng");
            }
        }

        [HttpGet("/search/{query}")]
        public async Task<ActionResult<IEnumerable<Department>>> Search(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                try
                {
                    var loggerResult = await _departmentSvc.Search(query.Trim().ToUpper());
                    return Ok(loggerResult);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Hãy điền nội dung tìm kiếm");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Department>> Update(Department department)
        {
            
            var loggerResult = await _departmentSvc.Update(department);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> Detele(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _departmentSvc.Delete(id);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(loggerResult);
                }
            }
            else
            {
                return BadRequest("Hãy điền ID để xoá đối tượng");
            }
        }
    }
}