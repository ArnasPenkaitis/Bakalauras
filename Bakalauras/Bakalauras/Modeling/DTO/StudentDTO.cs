using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;

namespace Bakalauras.Modeling.DTO
{
    public class StudentDTO : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
