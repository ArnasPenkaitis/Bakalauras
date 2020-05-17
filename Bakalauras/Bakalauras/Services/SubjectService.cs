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
    public class SubjectService : ISubjectService
    {
        private readonly IHubContext<PushingHub> _hubContext;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public SubjectService(IBaseRepository baseRepository, IMapper mapper,IHubContext<PushingHub> hubContext)
        {
            _hubContext = hubContext;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDTO>> GetEntities(int teacherId)
        {
            var entities = await _baseRepository.QueryAsync<Subject>();
            var filtered = entities.Where(x => x.Teacher.Id == teacherId);
            var mappedEntities = _mapper.Map<IEnumerable<SubjectDTO>>(filtered);
            return mappedEntities;
        }

        public async Task<SubjectDTO> GetEntityById(int id)
        {
            var entity = await _baseRepository.QueryByIdAsync<Subject>(id);
            var mappedEntity = _mapper.Map<SubjectDTO>(entity);
            return mappedEntity;
        }

        public async Task<SubjectDTO> CreateEntity(int teacherId, SubjectDTO data)
        {
            var entity = _mapper.Map<Subject>(data);
            entity.TeacherId = teacherId;
            var created = await _baseRepository.CreateAsync(entity);
            data = _mapper.Map<SubjectDTO>(created);
            await _hubContext.Clients.All.SendAsync(PushTypesEnum.subjectCreated.ToString(), data);
            return data;
        }

        public async Task<SubjectDTO> UpdateEntity( int id, SubjectDTO data)
        {
            var entityList = await _baseRepository.QueryAsync<Subject>();
            var result = entityList.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                data.Id = id;
                result.Name = data.Name;
                await _baseRepository.UpdateAsync(id, result);
                await _hubContext.Clients.All.SendAsync(PushTypesEnum.subjectUpdated.ToString(), data);
                return data;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }

        public async Task DeleteEntity(int id)
        {
            var entityList = await _baseRepository.QueryAsync<Subject>();

            var result = entityList.FirstOrDefault(x => x.Id == id);

            if (result != null)
            {
                await _baseRepository.DeleteAsync<Subject>(id);
                return;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }
    }
}
