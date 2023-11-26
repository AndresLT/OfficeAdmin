using OfficeAdmin_API.Models;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;

namespace OfficeAdmin_API.Services.Interfaces
{
    public interface IOfficeService
    {
        Task<Response<List<Office>>> GetOffices(bool all);
        Task<Response<string>> CreateOffice(CreateOfficeRequest office);
        Task<Response<string>> ModifyOffice(ModifyOfficeRequest office);
    }
}
