using Bakalauras.Modeling.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDTO>> GetEntities();
        Task<TeacherDTO> GetEntityById(int id);
        Task<TeacherDTO> CreateEntity(TeacherDTO data);
        Task<TeacherDTO> UpdateEntity(int id, TeacherDTO data);
        Task DeleteEntity(int id);
    }
}
