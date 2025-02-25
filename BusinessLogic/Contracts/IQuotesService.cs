using BusinessLogic.DTOs.Quotes;

namespace BusinessLogic.Contracts
{
    public interface IQuotesService
    {
        Task AddQuote(QuoteAddDTO model);
        Task DeleteQuote(Guid id);
        Task<QuoteDTO?> GetQuote(Guid id);
        Task<List<QuoteDTO>?> GetQuotes();
    }
}