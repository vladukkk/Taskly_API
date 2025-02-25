using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Quotes
{
    public class QuoteDTO : IQuote
    {
        public string Text { get; set; } = null!;
        public DateOnly CreatedAt { get; set; }
    }
}
