using BusinessLogic.DTOs.Task;

namespace BusinessLogic.Contracts
{
    public interface ITaskService
    {
        Task AddTask(TaskAddDTO task, string userId);
        Task DeleteTask(Guid id);
        Task ExecuteTask(Guid id);
        Task<TaskDTO> GetById(Guid id);
        Task<List<TaskDTO>> GetTasks();
        Task<List<TaskDTO>?> GetUserTasks(string userId);
        Task UpdateTask(TaskUpdateDTO task);
    }
}