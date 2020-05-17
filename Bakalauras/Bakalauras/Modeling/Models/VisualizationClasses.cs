using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;
using Castle.Components.DictionaryAdapter;

namespace Bakalauras.Modeling.Models
{
    public class VisualizationClasses: IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public int VisualizationId { get; set; }
        public int ClassId { get; set; }

        public virtual Visualization Visualization { get; set; }
        public virtual Class Classes { get; set; }
    }
}
