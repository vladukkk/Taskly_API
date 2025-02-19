
using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Tag
{
    public class TagAddDTO : ITag
    {
        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;
    }
}
