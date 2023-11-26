using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;

namespace OfficeAdmin_API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Response<UserResponse>> Login(string username, string password);
        Task<Response<string>> Logout(string username);
        Task<Response<List<UserAdminResponse>>> GetUsers();
        Task<Response<string>> CreateUser(CreateUserRequest newUser);
        Task<Response<string>> ModifyUser(ModifyUserRequest modifyUser);
        Task<Response<string>> ChangeUserPassword(ChangeUserPasswordRequest req);
        Task<Response<List<LogResponse>>> GetLogs();
    }
}
