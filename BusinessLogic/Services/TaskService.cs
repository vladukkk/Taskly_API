
using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Task;
using DataAccess.Contracts;
using DataAccess.EntityModels;
using System.Linq.Expressions;

namespace BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskEntity> _repository;
        private readonly IRepository<TagEntity> _tagRepository;
        private readonly IMapper _mapper;

        public TaskService(IRepository<TaskEntity> repository
            , IRepository<TagEntity> tagRepository
            , IMapper mapper)
        {
            _repository = repository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<List<TaskDTO>> GetTasks()
        {
            var includes = new Expression<Func<TaskEntity, object>>[]
            {
                t => t.Priority,
                t => t.Tags
            };

            var result = await _repository.Get(includeProperties: includes);

            return _mapper.Map<List<TaskDTO>>(result);
        }

        public async Task<TaskDTO> GetById(Guid id)
        {
            var result = await _repository.GetById(id, t => t.Priority, t => t.Tags);

            return _mapper.Map<TaskDTO>(result);
        }

        public async Task ExecuteTask(Guid id)
        {
            var taskEntity = await _repository.GetById(id);

            if (taskEntity == null)
                throw new InvalidOperationException($"Task with ID {id} not found");

            taskEntity.IsCompleted = true;
            await _repository.SaveAsync();
        }

        public async Task AddTask(TaskAddDTO task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);

            var existingTags = await _tagRepository.Get(tag => task.TagIds.Contains(tag.Id));
            taskEntity.Tags = _mapper.Map<List<TagEntity>>(existingTags);
            taskEntity.IsCompleted = false;

            await _repository.Add(taskEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateTask(TaskUpdateDTO task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);
            await _repository.Update(taskEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteTask(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}
