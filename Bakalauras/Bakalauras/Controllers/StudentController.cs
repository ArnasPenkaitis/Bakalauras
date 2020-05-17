using Bakalauras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;
using Bakalauras.Modeling.Models;

namespace Bakalauras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEntities()
        {
            try
            {
                IEnumerable<StudentDTO> students = await _studentService.GetEntities();
                return Ok(students);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult> GetEntityById(int studentId)
        {
            try
            {
                StudentDTO student = await _studentService.GetEntityById(studentId);
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{studentId}")]
        public async Task<ActionResult> DeleteEntityById(int studentId)
        {
            try
            {
                await _studentService.DeleteEntity(studentId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{studentId}")]
        public async Task<ActionResult> UpdateEntity(int studentId, [FromBody] StudentDTO student)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                StudentDTO result= await _studentService.UpdateEntity(studentId, student);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> CreateEntity([FromBody]StudentDTO student)
        {
            //if (HttpContext.Request.ContentType != "application/json")
                //return StatusCode(415);
            try
            {
                StudentDTO created = await _studentService.CreateEntity(student);
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public StatusCodeResult UpdateEntityBadUrl([FromBody] StudentDTO data)
        {
            return StatusCode(405);
        }

        [HttpPost("{id}")]
        public StatusCodeResult PostEntityBadUrl([FromBody] StudentDTO data)
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
