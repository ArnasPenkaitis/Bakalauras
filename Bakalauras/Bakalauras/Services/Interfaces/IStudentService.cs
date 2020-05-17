using Bakalauras.Modeling.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetEntities();
        Task<StudentDTO> GetEntityById(int id);
        Task<StudentDTO> CreateEntity(StudentDTO data);
        Task<StudentDTO> UpdateEntity(int id, StudentDTO data);
        Task DeleteEntity(int id);
    }
}
