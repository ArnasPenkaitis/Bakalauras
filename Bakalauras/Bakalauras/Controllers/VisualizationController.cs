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
    public class VisualizationController: ControllerBase
    {
        private readonly IVisualizationService _visualizationService;
        private readonly IBlobService _blobService;
        public VisualizationController(IVisualizationService visualizationService, IBlobService blobService)
        {
            _visualizationService = visualizationService;
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEntities()
        {
            try
            {
                IEnumerable<VisualizationDTO> visualizations = await _visualizationService.GetEntities();
                return Ok(visualizations);
;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("class/{classId}")]
        public async Task<ActionResult> GetEntitiesBySubject(int classId)
        {
            try
            {
                var visualizationList = await _visualizationService.GetEntitiesBySubject(classId);
                return Ok(visualizationList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("class/{classId}/{visualizationId}")]
        public async Task<ActionResult> DeleteEntityById(int classId, int visualizationId)
        {
            try
            {
                await _visualizationService.DeleteEntityChain(classId,visualizationId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{visualizationId}")]
        public async Task<ActionResult> GetEntityById(int visualizationId)
        {
            try
            {
                VisualizationDTO visualizationDto = await _visualizationService.GetEntityById(visualizationId);
                return Ok(visualizationDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{visualizationId}")]
        public async Task<ActionResult> DeleteEntityById(int visualizationId)
        {
            try
            {
                await _visualizationService.DeleteEntity(visualizationId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{visualizationId}")]
        public async Task<ActionResult> UpdateEntity(int visualizationId, [FromBody] VisualizationDTO visualizationDto)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                VisualizationDTO result = await _visualizationService.UpdateEntity(visualizationId, visualizationDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("{filename}")]
        public async Task<ActionResult> CreateEntity([FromBody]VisualizationDTO visualizationDto ,string filename)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                VisualizationDTO created = await _visualizationService.CreateEntity(visualizationDto,visualizationDto.content,filename);
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("class/{classId}/{visualizationId}")]
        public async Task<ActionResult> CreateEntityChain(int classId, int visualizationId)
        {
            //if (HttpContext.Request.ContentType != "application/json")
            //    return StatusCode(415);
            try
            {
                await _visualizationService.CreateEntityChain(classId,visualizationId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public StatusCodeResult UpdateEntityBadUrl([FromBody] VisualizationDTO data)
        {
            return StatusCode(405);
        }

        [HttpPost("{id}")]
        public StatusCodeResult PostEntityBadUrl([FromBody] VisualizationDTO data)
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
