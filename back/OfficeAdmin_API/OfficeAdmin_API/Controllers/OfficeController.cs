using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeAdmin_API.Models.Response;
using OfficeAdmin_API.Services.Interfaces;

namespace OfficeAdmin_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpGet]
        public async Task<Response<string>> GetOffices()
        {
            Response<string> response = new Response<string>();
            try
            {
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = ex.Message;
                response.Result = null;
                return response;
            }
        }
    }
}
