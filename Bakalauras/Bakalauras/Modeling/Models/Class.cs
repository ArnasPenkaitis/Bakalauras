using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;

namespace Bakalauras.Modeling.Models
{
    public class Class : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public int SubjectId { get; set; }
        public virtual ICollection<VisualizationClasses> VisualizationClasses { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
