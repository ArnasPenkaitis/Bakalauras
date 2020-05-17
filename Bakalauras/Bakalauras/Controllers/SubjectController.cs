using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;
using Bakalauras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bakalauras.Controllers
{
    [Route("api/teacher/{teacherId}/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEntities(int teacherId)
        {
            try
            {
                IEnumerable<SubjectDTO> subjects = await _subjectService.GetEntities(teacherId);
                return Ok(subjects);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{subjectId}")]
        public async Task<ActionResult> GetEntityById(int subjectId)
        {
            try
            {
                SubjectDTO subjectDto = await _subjectService.GetEntityById(subjectId);
                return Ok(subjectDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{subjectId}")]
        public async Task<ActionResult> DeleteEntityById(int subjectId)
        {
            try
            {
                await _subjectService.DeleteEntity(subjectId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{subjectId}")]
        public async Task<ActionResult> UpdateEntity(int subjectId, [FromBody] SubjectDTO subjectDto)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                SubjectDTO result = await _subjectService.UpdateEntity( subjectId, subjectDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEntity(int teacherId, [FromBody]SubjectDTO subjectDto)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                SubjectDTO created = await _subjectService.CreateEntity(teacherId, subjectDto);
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public StatusCodeResult UpdateEntityBadUrl([FromBody] SubjectDTO data)
        {
            return StatusCode(405);
        }

        [HttpPost("{id}")]
        public StatusCodeResult PostEntityBadUrl([FromBody] SubjectDTO data)
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
