using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bakalauras.Modeling.DTO;
using Bakalauras.Modeling.Models;
using Bakalauras.Services.Interfaces;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;

namespace Bakalauras.Services
{
    public class StudentService: IStudentService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public StudentService(IBaseRepository baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<StudentDTO>> GetEntities()
        {
            var entities = await _baseRepository.QueryAsync<Student>();
            var mappedEntities = _mapper.Map<IEnumerable<StudentDTO>>(entities);
            return mappedEntities;
        }

        public async Task<StudentDTO> GetEntityById(int id)
        {
            var entity = await _baseRepository.QueryByIdAsync<Student>(id);
            var mappedEntity = _mapper.Map<StudentDTO>(entity);
            return mappedEntity;
        }

        public async Task<StudentDTO> CreateEntity(StudentDTO data)
        {
            var entities = await _baseRepository.QueryAsync<Student>();

            foreach (var element in entities)
            {
                if(element.Username == data.Username)
                    throw new Exception("Student with username: " + data.Username + " exists.");
            }
            var entity = _mapper.Map<Student>(data);
            var created = await _baseRepository.CreateAsync(entity);
            data = _mapper.Map<StudentDTO>(created);
            return data;
        }

        public async Task<StudentDTO> UpdateEntity(int id, StudentDTO data)
        {
            var entityList = await _baseRepository.QueryAsync<Student>();
            var result = entityList.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                data.Id = id;
                var entity = _mapper.Map<Student>(data);
                await _baseRepository.UpdateAsync(id, entity);
                return data;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }

        public async Task DeleteEntity(int id)
        {
            var entityList = await _baseRepository.QueryAsync<Student>();

            var result = entityList.FirstOrDefault(x => x.Id == id);

            if (result != null)
            {
                await _baseRepository.DeleteAsync<Student>(id);
                return;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }

    }
}
