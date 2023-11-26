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
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpGet("{all}")]
        public async Task<Response<List<Office>>> GetOffices(bool all)
        {
            Response<List<Office>> response = new Response<List<Office>>();
            try
            {
                var res = await _officeService.GetOffices(all);
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
        public async Task<Response<string>> CreateOffice([FromBody] CreateOfficeRequest req)
        {
            Response<string> response = new Response<string>();
            try
            {
                var res = await _officeService.CreateOffice(req);
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
        public async Task<Response<string>> ModifyOffice([FromBody] ModifyOfficeRequest req)
        {
            Response<string> response = new Response<string>();
            try
            {
                var res = await _officeService.ModifyOffice(req);
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
