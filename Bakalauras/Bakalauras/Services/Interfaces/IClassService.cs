using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;

namespace Bakalauras.Services.Interfaces
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDTO>> GetEntities(int teacherId, int subjectId);
        Task<ClassDTO> GetEntityById(int id);
        Task<ClassDTO> CreateEntity(int teacherId, int subjectId, ClassDTO data);
        Task<ClassDTO> UpdateEntity(int id, ClassDTO data);
        Task DeleteEntity(int id);
    }
}
