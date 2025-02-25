
using DataAccess.Contracts;

namespace DataAccess.EntityModels
{
    public class QuoteEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = null!;
        public DateOnly CreatedAt { get; set; }
    }
}
