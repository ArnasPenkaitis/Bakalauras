using Bakalauras.Modeling.Models;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalauras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        protected readonly IBaseRepository _repository;

        public AuthController(IBaseRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("token/teacher")]
        public virtual async Task<ActionResult> GetTokenIfLogged([FromBody] Teacher teacher)
        {

            var userEntities = await _repository.QueryAsync<Teacher>();

            foreach (var entity in userEntities)
            {
                if (entity.Username.Equals(teacher.Username) && entity.Password.Equals(teacher.Password))
                {
                    string securityKey = "this_is_my_super_long_security_key";

                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken(
                        issuer: "bakalauras",
                        audience: "readers",
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials
                        );
                    var tokenas = new JwtSecurityTokenHandler().WriteToken(token);
                    var siunciu = new { tokenas, entity.Id };
                    return Ok(siunciu);
                }
            }
            return Unauthorized();
        }

        [HttpPost("token/student")]
        public virtual async Task<ActionResult> GetTokenIfLogged([FromBody] Student student)
        {

            var userEntities = await _repository.QueryAsync<Student>();

            foreach (var entity in userEntities)
            {
                if (entity.Username.Equals(student.Username) && entity.Password.Equals(student.Password))
                {
                    string securityKey = "this_is_my_super_long_security_key";

                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken(
                        issuer: "bakalauras",
                        audience: "readers",
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials
                        );
                    var Tokenas = new JwtSecurityTokenHandler().WriteToken(token);
                    var siunciu = new { Tokenas, entity.Id };
                    return Ok(siunciu);
                }
            }
            return Unauthorized();
        }
    }
}
