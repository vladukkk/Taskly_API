using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Contracts
{
    public interface ITagService
    {
        Task<List<TagDTO>?> GetTags();
        Task<TagDTO?> GetById(Guid id);
        Task AddTag(TagAddDTO tag);
        Task UpdateTag(TagUpdateDTO tag);
        Task DeleteTag(Guid Id);
    }
}