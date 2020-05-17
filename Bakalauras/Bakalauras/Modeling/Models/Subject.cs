using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;

namespace Bakalauras.Modeling.Models
{
    public class Subject :IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher  { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}
