using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;
using OfficeAdmin_API.Services.Interfaces;

namespace OfficeAdmin_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("{username}/{password}")]
        public async Task<Response<UserResponse>> Login(string username, string password)
        {
            Response<UserResponse> response = new Response<UserResponse>();
            try
            {
                var result = await _adminService.Login(username, password);
                response = result;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = ex.Message;
                response.Result = new UserResponse();
            }

            return response;
        }

        [HttpGet("{username}")]
        public async Task<Response<string>> Logout(string username)
        {
            Response<string> response = new Response<string>();
            try
            {
                var result = await _adminService.Logout(username);
                response = result;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = ex.Message;
                response.Result = null;
            }

            return response;
        }

        [HttpGet]
        public async Task<Response<List<UserAdminResponse>>> GetUsers()
        {
            Response<List<UserAdminResponse>> response = new Response<List<UserAdminResponse>>();
            try
            {
                var result = await _adminService.GetUsers();
                response = result;
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
        public async Task<Response<string>> CreateUser([FromBody] CreateUserRequest user)
        {
            Response<string> response = new Response<string>();
            try
            {
                var result = await _adminService.CreateUser(user);
                response = result;
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
        public async Task<Response<string>> ModifyUser([FromBody] ModifyUserRequest user)
        {
            Response<string> response = new Response<string>();
            try
            {
                var result = await _adminService.ModifyUser(user);
                response = result;
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
        public async Task<Response<string>> ChangeUserPassword([FromBody] ChangeUserPasswordRequest req)
        {
            Response<string> response = new Response<string>();
            try
            {
                var result = await _adminService.ChangeUserPassword(req);
                response = result;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = ex.Message;
                response.Result = null;
            }

            return response;
        }

        [HttpGet]
        public async Task<Response<List<LogResponse>>> GetLogs()
        {
            Response<List<LogResponse>> response = new Response<List<LogResponse>>();
            try
            {
                var result = await _adminService.GetLogs();
                response = result;
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
