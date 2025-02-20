using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Task;
using BusinessLogic.Exceptions;
using DataAccess.Contracts;
using DataAccess.EntityModels;
using System.Linq.Expressions;

namespace BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskEntity> _repository;
        private readonly IRepository<TagEntity> _tagRepository;
        private readonly IRepository<PriorityEntity> _priorityRepository;
        private readonly IMapper _mapper;

        public TaskService(IRepository<TaskEntity> repository
            , IRepository<TagEntity> tagRepository
            , IRepository<PriorityEntity> priorityRepository
            , IMapper mapper)
        {
            _repository = repository;
            _tagRepository = tagRepository;
            _priorityRepository = priorityRepository;
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

        private async Task<(List<TagEntity>, PriorityEntity)> CheckExistingEntities(TaskAddDTO task)
        {
            var existingTags = await _tagRepository.Get(tag => task.TagIds.Contains(tag.Id));
            if (existingTags == null || !existingTags.Any())
                throw new TaskNotFoundException($"Tags with IDs {string.Join(", ", task.TagIds)} not found.");

            var missingTags = task.TagIds.Except(existingTags.Select(t => t.Id)).ToList();

            var existingPriority = await _priorityRepository.GetById(task.PriorityId);
            if (existingPriority == null)
                throw new TaskNotFoundException($"Priority with id {task.PriorityId} not found");

            if (missingTags.Any())
            {
                throw new TaskNotFoundException($"Tags with IDs {string.Join(", ", missingTags)} not found.");
            }

            return (existingTags.ToList(), existingPriority);
        }

        public async Task AddTask(TaskAddDTO task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);

            var(existingTags, existingPriority) = await CheckExistingEntities(task);

            taskEntity.Tags = _mapper.Map<List<TagEntity>>(existingTags);
            taskEntity.Priority = existingPriority;
            taskEntity.IsCompleted = false;
            taskEntity.CreatedAt = DateTime.Now;
            taskEntity.UpdatedAt = DateTime.Now;

            await _repository.Add(taskEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateTask(TaskUpdateDTO task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);

            var (existingTags, existingPriority) = await CheckExistingEntities(_mapper.Map<TaskAddDTO>(task));
            taskEntity.Priority = existingPriority;
            taskEntity.Tags = existingTags;
            taskEntity.UpdatedAt = DateTime.Now;

            await _repository.Update(taskEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteTask(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}
