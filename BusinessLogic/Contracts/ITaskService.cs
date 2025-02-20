using BusinessLogic.DTOs.Task;

namespace BusinessLogic.Contracts
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetTasks();
        Task<TaskDTO> GetById(Guid id);
        Task AddTask(TaskAddDTO task);
        Task ExecuteTask(Guid task);
        Task DeleteTask(Guid id);
        Task UpdateTask(TaskUpdateDTO task);
    }
}