using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;

namespace Bakalauras.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetEntities(int teacherId);
        Task<SubjectDTO> GetEntityById(int id);
        Task<SubjectDTO> CreateEntity(int teacherId,SubjectDTO data);
        Task<SubjectDTO> UpdateEntity(int id, SubjectDTO data);
        Task DeleteEntity(int id);
    }
}
