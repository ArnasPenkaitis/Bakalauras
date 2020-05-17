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
    public class TeacherService: ITeacherService
    {
        private readonly IHubContext<PushingHub> _hubContext;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public TeacherService(IBaseRepository baseRepository, IMapper mapper, IHubContext<PushingHub> hubContext)
        {
            _hubContext = hubContext;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TeacherDTO>> GetEntities()
        {
            var entities = await _baseRepository.QueryAsync<Teacher>();
            var mappedEntities = _mapper.Map<IEnumerable<TeacherDTO>>(entities);
            return mappedEntities;
        }

        public async Task<TeacherDTO> GetEntityById(int id)
        {
            var entity = await _baseRepository.QueryByIdAsync<Teacher>(id);
            var mappedEntity = _mapper.Map<TeacherDTO>(entity);
            return mappedEntity;
        }

        public async Task<TeacherDTO> CreateEntity(TeacherDTO data)
        {
            var entity = _mapper.Map<Teacher>(data);
            var created = await _baseRepository.CreateAsync(entity);
            data = _mapper.Map<TeacherDTO>(created);
            await _hubContext.Clients.All.SendAsync(PushTypesEnum.accountCreated.ToString(), data);
            return data;
        }

        public async Task<TeacherDTO> UpdateEntity(int id, TeacherDTO data)
        {
            var entityList = await _baseRepository.QueryAsync<Teacher>();
            var result = entityList.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                data.Id = id;
                result.Name = data.Name;
                result.Surname = data.Surname;
                result.Email = data.Email;
                result.Password = data.Password;
                await _baseRepository.UpdateAsync(id, result);
                await _hubContext.Clients.All.SendAsync(PushTypesEnum.accountUpdated.ToString(), data);
                return data;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }

        public async Task DeleteEntity(int id)
        {
            var entityList = await _baseRepository.QueryAsync<Teacher>();

            var result = entityList.FirstOrDefault(x => x.Id == id);

            if (result != null)
            {
                await _baseRepository.DeleteAsync<Teacher>(id);
                return;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }
    }
}
