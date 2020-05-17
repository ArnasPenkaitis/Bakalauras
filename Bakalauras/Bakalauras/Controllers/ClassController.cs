using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;
using Bakalauras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bakalauras.Controllers
{
    [Route("api/teacher/{teacherId}/subject/{subjectId}/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEntities(int teacherId, int subjectId)
        {
            try
            {
                IEnumerable<ClassDTO> classes = await _classService.GetEntities(teacherId, subjectId);
                return Ok(classes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{classId}")]
        public async Task<ActionResult> GetEntityById(int classId)
        {
            try
            {
                ClassDTO classDto = await _classService.GetEntityById(classId);
                return Ok(classDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{classId}")]
        public async Task<ActionResult> DeleteEntityById(int classId)
        {
            try
            {
                await _classService.DeleteEntity(classId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{classId}")]
        public async Task<ActionResult> UpdateEntity(int classId, [FromBody] ClassDTO classDto)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                ClassDTO result = await _classService.UpdateEntity(classId, classDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEntity(int teacherId, int subjectId,[FromBody]ClassDTO classDto)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                ClassDTO created = await _classService.CreateEntity(teacherId, subjectId,classDto);
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public StatusCodeResult UpdateEntityBadUrl([FromBody] ClassDTO data)
        {
            return StatusCode(405);
        }

        [HttpPost("{id}")]
        public StatusCodeResult PostEntityBadUrl([FromBody] ClassDTO data)
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
