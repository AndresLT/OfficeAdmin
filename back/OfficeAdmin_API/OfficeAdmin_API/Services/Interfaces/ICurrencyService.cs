using OfficeAdmin_API.Models;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;

namespace OfficeAdmin_API.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<Response<List<Currency>>> GetCurrencies(bool all);
        Task<Response<string>> CreateCurrency(CreateCurrencyRequest currency);
        Task<Response<string>> ModifyCurrency(ModifyCurrencyRequest currency);
    }
}
