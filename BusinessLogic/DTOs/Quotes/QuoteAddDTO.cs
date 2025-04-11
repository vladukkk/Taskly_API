using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Quotes
{
    public class QuoteAddDTO : IQuote
    {
        public string Text { get; set; } = null!;
    }
}
