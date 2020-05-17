using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;

namespace Bakalauras.Services.Interfaces
{
    public interface IVisualizationService
    {
        Task<IEnumerable<VisualizationDTO>> GetEntities();
        Task<IEnumerable<VisualizationDTO>> GetEntitiesBySubject(int subjectId);
        Task<VisualizationDTO> GetEntityById(int id);
        Task<VisualizationDTO> CreateEntity(VisualizationDTO data,string content, string filename);
        Task CreateEntityChain(int classId, int visualizationId);
        Task<VisualizationDTO> UpdateEntity(int id, VisualizationDTO data);
        Task DeleteEntity(int id);
        Task DeleteEntityChain(int classId, int id);
    }
}
