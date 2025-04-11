using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Contracts
{
    public interface ITagService
    {
        Task<List<TagDTO>?> GetTags();
        Task<List<TagDTO>?> GetTags(string userId);
        Task<TagDTO?> GetById(Guid id);
        Task AddGlobalTag(TagAddDTO tag);
        Task AddPersonalityTag(TagAddDTO tag, string userId);
        Task UpdateTag(TagUpdateDTO tag);
        Task DeleteTag(Guid Id);
    }
}