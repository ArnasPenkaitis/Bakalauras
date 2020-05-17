using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;

namespace Bakalauras.Modeling.DTO
{
    public class ClassDTO : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
