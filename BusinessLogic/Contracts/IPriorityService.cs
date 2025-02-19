using BusinessLogic.DTOs.Priority;

namespace BusinessLogic.Contracts
{
    public interface IPriorityService
    {
        Task<List<PriorityDTO>?> GetPriorities();
        Task<PriorityDTO?> GetById(Guid id);
        Task AddPriority(PriorityAddDTO priority);
        Task UpdatePriority(PriorityUpdateDTO priority);
        Task DeletePriority(Guid Id);
    }
}