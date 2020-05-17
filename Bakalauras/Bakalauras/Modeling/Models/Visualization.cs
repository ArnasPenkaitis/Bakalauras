using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;

namespace Bakalauras.Modeling.Models
{
    public class Visualization : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string FileUrl { get; set; }

        public virtual ICollection<VisualizationClasses> VisualizationClasses { get; set; }
    }
}
