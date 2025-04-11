using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Quotes;
using DataAccess.Contracts;
using DataAccess.EntityModels;

namespace BusinessLogic.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly IRepository<QuoteEntity> _repository;
        private readonly IMapper _mapper;

        public QuotesService(IRepository<QuoteEntity> repository
            , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<QuoteDTO>?> GetQuotes()
        {
            var quotes = await _repository.Get();
            return _mapper.Map<List<QuoteDTO>>(quotes);
        }

        public async Task<QuoteDTO?> GetQuote(Guid id)
        {
            var quote = await _repository.GetById(id);
            return _mapper.Map<QuoteDTO>(quote);
        }

        public async Task AddQuote(QuoteAddDTO model)
        {
            var quote = _mapper.Map<QuoteEntity>(model);
            quote.CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            await _repository.Add(quote);
            await _repository.SaveAsync();
        }

        public async Task DeleteQuote(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}
