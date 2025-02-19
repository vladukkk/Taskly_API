using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Priority;
using DataAccess.Contracts;
using DataAccess.EntityModels;

namespace BusinessLogic.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IRepository<PriorityEntity> _priorityRepository;
        private readonly IMapper _mapper;

        public PriorityService(IRepository<PriorityEntity> repository
            , IMapper mapper)
        {
            _priorityRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<PriorityDTO>?> GetPriorities()
        {
            var result = await _priorityRepository.Get();
            return _mapper.Map<List<PriorityDTO>>(result);
        }

        public async Task<PriorityDTO?> GetById(Guid id)
        {
            var priority = await _priorityRepository.GetById(id);
            return _mapper.Map<PriorityDTO>(priority);
        }

        public async Task AddPriority(PriorityAddDTO priority)
        {
            var priorityEntity = _mapper.Map<PriorityEntity>(priority);
            await _priorityRepository.Add(priorityEntity);
            await _priorityRepository.SaveAsync();
        }

        public async Task UpdatePriority(PriorityUpdateDTO priority)
        {
            var priorityEntity = _mapper.Map<PriorityEntity>(priority);
            await _priorityRepository.Update(priorityEntity);
            await _priorityRepository.SaveAsync();
        }

        public async Task DeletePriority(Guid Id)
        {
            await _priorityRepository.Delete(Id);
        }

    }
}
