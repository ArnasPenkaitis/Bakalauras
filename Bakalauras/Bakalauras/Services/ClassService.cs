using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bakalauras.Modeling.DTO;
using Bakalauras.Modeling.Models;
using Bakalauras.Services.Interfaces;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;
using Bakalauras.Shared.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Bakalauras.Services
{
    public class ClassService : IClassService
    {
        private readonly IHubContext<PushingHub> _hubContext;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public ClassService(IBaseRepository baseRepository, IMapper mapper, IHubContext<PushingHub> hubContext)
        {
            _hubContext = hubContext;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassDTO>> GetEntities(int teacherId, int subjectId)
        {
            var entities = await _baseRepository.QueryAsync<Class>();
            var filtered = entities.Where(x => x.Subject.Id == subjectId);
            var mappedEntities = _mapper.Map<IEnumerable<ClassDTO>>(filtered);
            return mappedEntities;
        }

        public async Task<ClassDTO> GetEntityById(int id)
        {
            var entity = await _baseRepository.QueryByIdAsync<Class>(id);
            var mappedEntity = _mapper.Map<ClassDTO>(entity);
            return mappedEntity;
        }

        public async Task<ClassDTO> CreateEntity(int teacherId, int subjectId,ClassDTO data)
        {
            var entity = _mapper.Map<Class>(data);
            entity.SubjectId = subjectId;
            var created = await _baseRepository.CreateAsync(entity);
            data = _mapper.Map<ClassDTO>(created);
            await _hubContext.Clients.All.SendAsync(PushTypesEnum.lessonCreated.ToString(), data);
            return data;
        }

        public async Task<ClassDTO> UpdateEntity(int id, ClassDTO data)
        {
            var entityList = await _baseRepository.QueryAsync<Class>();
            var result = entityList.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                data.Id = id;
                result.Abbreviation = data.Abbreviation;
                result.Name = data.Name;
                await _baseRepository.UpdateAsync(id, result);
                await _hubContext.Clients.All.SendAsync(PushTypesEnum.lessonUpdated.ToString(), data);
                return data;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }

        public async Task DeleteEntity(int id)
        {
            var entityList = await _baseRepository.QueryAsync<Class>();

            var result = entityList.FirstOrDefault(x => x.Id == id);

            if (result != null)
            {
                await _baseRepository.DeleteAsync<Class>(id);
                return;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }
    }
}
