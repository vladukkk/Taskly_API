using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Contracts
{
    public interface ITagService
    {
        Task AddTag(TagAddDTO tag);
        Task<List<TagDTO>> GetTags();
        Task UpdateTag(TagUpdateDTO tag);
        Task DeleteTag(Guid Id);
    }
}