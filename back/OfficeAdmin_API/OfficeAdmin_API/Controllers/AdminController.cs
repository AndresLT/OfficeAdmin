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

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
        public async Task<Response<string>> CreateUser(CreateUserRequest user)
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
        public async Task<Response<string>> ModifyUser(ModifyUserRequest user)
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
    }
}
