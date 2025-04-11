using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Priority;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.DTOs.User;
using DataAccess.Contracts;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepository<TaskEntity> _tasksRepository;
        private readonly IMapper _mapper;

        public AccountService(UserManager<UserEntity> userManager
            , IRepository<TaskEntity> tasksRepository
            , IMapper mapper)
        {
            _userManager = userManager;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO?> GetCurrentUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserDTO>(user);
        }

        private List<TagStatsDTO>? GetTopUserTags(IEnumerable<TaskEntity>tasks, uint top = 3)
        {
            var allTags = tasks
                .SelectMany(t => t.Tags ?? new List<TagEntity>())
                .GroupBy(tag => tag.Id)
                .Select(group => new TagStatsDTO
                {
                    Tag = new TagDTO
                    {
                        Id = group.First().Id,
                        Title = group.First().Title,
                        ColorHash = group.First().ColorHash
                    },
                    TagCount = group.Count()
                })
                .OrderByDescending(tag => tag.TagCount)
                .Take((int)top)
                .ToList();

            return allTags;
        }

        private List<PriorityStatsDTO> GetTopPriorities(IEnumerable<TaskEntity> tasks)
        {
            return tasks
                .Select(t => t.Priority)
                .GroupBy(p => p.Id)
                .Select(group => new PriorityStatsDTO
                {
                    Priority = new PriorityDTO
                    {
                        Id = group.Key,
                        Title = group.First().Title,
                        ColorHash = group.First().ColorHash
                    },
                    PriorityCount = group.Count()
                })
                .OrderByDescending(p => p.PriorityCount)
                .ToList();
        }


        public async Task<UserStatsDTO?> GetUserStats(string userId)
        {
            var include = new Expression<Func<TaskEntity, object>>[] {
                entity => entity.Priority,
                entity => entity.Tags
            };

            var tasks = await _tasksRepository.Get(
                filter: task => task.UserId == userId,
                includeProperties: include
            );

            tasks = tasks.ToList();
            int doneTaskCount = tasks.Where(t => t.IsCompleted).Count();
            var result = new UserStatsDTO
            {
                TasksCount = tasks.Count(),
                DoneTaskCount = doneTaskCount,
                TagStats = GetTopUserTags(tasks, 3),
                PriorityStats = GetTopPriorities(tasks)
            };

            return result;
        }
    }
}
