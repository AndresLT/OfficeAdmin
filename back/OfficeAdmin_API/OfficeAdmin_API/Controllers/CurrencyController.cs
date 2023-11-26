using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeAdmin_API.Models;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;
using OfficeAdmin_API.Services.Interfaces;

namespace OfficeAdmin_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<Response<List<Currency>>> GetCurrencies(bool all)
        {
            Response<List<Currency>> response = new Response<List<Currency>>();
            try
            {
                var res = await _currencyService.GetCurrencies(all);
                response = res;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = ex.Message;
                response.Result = null;
            }
            return response;
        }

        [HttpPost]
        public async Task<Response<string>> CreateCurrency(CreateCurrencyRequest req)
        {
            Response<string> response = new Response<string>();
            try
            {
                var res = await _currencyService.CreateCurrency(req);
                response = res;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = ex.Message;
                response.Result = null;
            }
            return response;
        }

        [HttpPost]
        public async Task<Response<string>> ModifyCurrency(ModifyCurrencyRequest req)
        {
            Response<string> response = new Response<string>();
            try
            {
                var res = await _currencyService.ModifyCurrency(req);
                response = res;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = ex.Message;
                response.Result = null;
            }
            return response;
        }
    }
}
