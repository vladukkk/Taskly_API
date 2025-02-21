using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Task;
using BusinessLogic.Exceptions;
using DataAccess.Contracts;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskEntity> _taskRepository;
        private readonly IRepository<TagEntity> _tagRepository;
        private readonly IRepository<PriorityEntity> _priorityRepository;
        private readonly IMapper _mapper;

        public TaskService(IRepository<TaskEntity> repository
            , IRepository<TagEntity> tagRepository
            , IRepository<PriorityEntity> priorityRepository
            , IHttpContextAccessor httpContextAccessor
            , IMapper mapper)
        {
            _taskRepository = repository;
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

            var result = await _taskRepository.Get(includeProperties: includes);

            return _mapper.Map<List<TaskDTO>>(result);
        }

        public async Task<List<TaskDTO>?> GetUserTasks(string userId)
        {
            var includes = new Expression<Func<TaskEntity, object>>[]
            {
                t => t.Priority,
                t => t.Tags
            };

            var userTasks = await _taskRepository.Get(
                filter: users => users.UserId == userId
                , includeProperties: includes);

            return _mapper.Map<List<TaskDTO>>(userTasks);
        }

        public async Task<TaskDTO> GetById(Guid id)
        {
            var result = await _taskRepository.GetById(id, t => t.Priority, t => t.Tags);

            return _mapper.Map<TaskDTO>(result);
        }

        public async Task ExecuteTask(Guid id)
        {
            var taskEntity = await _taskRepository.GetById(id);

            if (taskEntity == null)
                throw new InvalidOperationException($"Task with ID {id} not found");

            taskEntity.IsCompleted = true;
            await _taskRepository.SaveAsync();
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

        public async Task AddTask(TaskAddDTO task, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new TaskNotFoundException("user not found");

            var taskEntity = _mapper.Map<TaskEntity>(task);

            var (existingTags, existingPriority) = await CheckExistingEntities(task);

            taskEntity.Tags = _mapper.Map<List<TagEntity>>(existingTags);
            taskEntity.Priority = existingPriority;
            taskEntity.IsCompleted = false;
            taskEntity.CreatedAt = DateTime.Now;
            taskEntity.UpdatedAt = DateTime.Now;
            taskEntity.UserId = userId;

            await _taskRepository.Add(taskEntity);
            await _taskRepository.SaveAsync();

        }

        public async Task UpdateTask(TaskUpdateDTO task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);

            var (existingTags, existingPriority) = await CheckExistingEntities(_mapper.Map<TaskAddDTO>(task));
            taskEntity.Priority = existingPriority;
            taskEntity.Tags = existingTags;
            taskEntity.UpdatedAt = DateTime.Now;

            await _taskRepository.Update(taskEntity);
            await _taskRepository.SaveAsync();
        }

        public async Task DeleteTask(Guid id)
        {
            await _taskRepository.Delete(id);
        }
    }
}
