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
using Microsoft.VisualBasic;

namespace Bakalauras.Services
{
    public class VisualizationService : IVisualizationService
    {
        private readonly IHubContext<PushingHub> _hubContext;
        private readonly IBlobService _blobService;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public VisualizationService(IBaseRepository baseRepository, IMapper mapper,IBlobService blobService, IHubContext<PushingHub> hubContext)
        {
            _hubContext = hubContext;
            _blobService = blobService;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VisualizationDTO>> GetEntities()
        {
            var entities = await _baseRepository.QueryAsync<Visualization>();
            var mappedEntities = _mapper.Map<IEnumerable<VisualizationDTO>>(entities);
            return mappedEntities;
        }

        public async Task<IEnumerable<VisualizationDTO>> GetEntitiesBySubject(int classId)
        {
            var chainEntities = await _baseRepository.QueryAsync<VisualizationClasses>();
            var filteredChainEntities = chainEntities.Where(x => x.ClassId == classId);
            var visualizationList = new List<VisualizationDTO>();
            foreach (var ent in filteredChainEntities)
            {
                var temp = await _baseRepository.QueryByIdAsync<Visualization>(ent.VisualizationId);
                var mapped = _mapper.Map<VisualizationDTO>(temp);
                visualizationList.Add(mapped);
            }
            return visualizationList;
        }

        public async Task<VisualizationDTO> GetEntityById(int id)
        {
            var entity = await _baseRepository.QueryByIdAsync<Visualization>(id);
            var mappedEntity = _mapper.Map<VisualizationDTO>(entity);
            return mappedEntity;
        }

        public async Task<VisualizationDTO> CreateEntity(VisualizationDTO data, string content, string filename)
        {
            data.FileUrl = "https://arnpen.blob.core.windows.net/gltfs/" + filename;
            var entity = _mapper.Map<Visualization>(data);

            await _blobService.UploadBlob(content, filename);

            var created = await _baseRepository.CreateAsync(entity);
            data = _mapper.Map<VisualizationDTO>(created);
            await _hubContext.Clients.All.SendAsync(PushTypesEnum.visualizationCreated.ToString(), data);
            return data;
        }

        public async Task CreateEntityChain(int classId, int visualizationId)
        {
            var entity = new VisualizationClasses(){ClassId = classId, VisualizationId = visualizationId};
            var created = await _baseRepository.CreateAsync(entity);
            return;
        }

        public async Task<VisualizationDTO> UpdateEntity(int id, VisualizationDTO data)
        {
            var entityList = await _baseRepository.QueryAsync<Visualization>();
            var result = entityList.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                data.Id = id;
                result.Name = data.Name;
                result.Description = data.Description;
                result.FileUrl = data.FileUrl;
                await _baseRepository.UpdateAsync(id, result);
                await _hubContext.Clients.All.SendAsync(PushTypesEnum.visualizationUpdated.ToString(), data);
                return data;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }

        public async Task DeleteEntity(int id)
        {
            var entityList = await _baseRepository.QueryAsync<Visualization>();

            var result = entityList.FirstOrDefault(x => x.Id == id);

            if (result != null)
            {
                await _baseRepository.DeleteAsync<Visualization>(id);
                return;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }

        public async Task DeleteEntityChain(int classId, int id)
        {
            var entityList = await _baseRepository.QueryAsync<VisualizationClasses>();

            var result = entityList.FirstOrDefault(x => x.VisualizationId == id && x.ClassId == classId);

            if (result != null)
            {
                await _baseRepository.DeleteAsync<VisualizationClasses>(result.Id);
                return;
            }

            throw new Exception($"Entity with id: {id} not found.");
        }
    }
}
