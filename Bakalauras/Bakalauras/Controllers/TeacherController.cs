using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;
using Bakalauras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bakalauras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController: ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEntities()
        {
            try
            {
                IEnumerable<TeacherDTO> teachers = await _teacherService.GetEntities();
                return Ok(teachers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{teacherId}")]
        public async Task<ActionResult> GetEntityById(int teacherId)
        {
            try
            {
                TeacherDTO teacher = await _teacherService.GetEntityById(teacherId);
                return Ok(teacher);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{teacherId}")]
        public async Task<ActionResult> DeleteEntityById(int teacherId)
        {
            try
            {
                await _teacherService.DeleteEntity(teacherId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{teacherId}")]
        public async Task<ActionResult> UpdateEntity(int teacherId, [FromBody] TeacherDTO teacher)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                TeacherDTO result = await _teacherService.UpdateEntity(teacherId, teacher);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEntity([FromBody]TeacherDTO teacher)
        {
           // if (HttpContext.Request.ContentType != "application/json")
           //     return StatusCode(415);
            try
            {
                TeacherDTO created = await _teacherService.CreateEntity(teacher);
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public StatusCodeResult UpdateEntityBadUrl([FromBody] TeacherDTO data)
        {
            return StatusCode(405);
        }

        [HttpPost("{id}")]
        public StatusCodeResult PostEntityBadUrl([FromBody] TeacherDTO data)
        {
            return StatusCode(405);
        }

        [HttpDelete]
        public StatusCodeResult DeleteEntityBadUrl()
        {
            return StatusCode(405);
        }
    }
}
