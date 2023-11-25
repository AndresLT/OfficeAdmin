using OfficeAdmin_API.Models;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;
using OfficeAdmin_API.Services.Interfaces;

namespace OfficeAdmin_API.Services
{
    public class OfficeService: IOfficeService
    {
        private readonly TestDBContext _db;

        public OfficeService(TestDBContext db)
        {
            _db = db;
        }

        public async Task<Response<List<Office>>> GetOffices()
        {
            Response<List<Office>> response = new Response<List<Office>>();
            try
            {
                var res = _db.Offices.ToList();

                if(res.Count == 0)
                {
                    response.Status = "info";
                    response.Message = "No se encontraron sucursales.";
                    response.Result = res;
                }
                else
                {
                    response.Status = "success";
                    response.Message = "Sucursales consultadas correctamente";
                    response.Result = res;
                }

                
            }catch(Exception ex)
            {
                response.Status = "error";
                response.Message = "Ocurrio un error consultando sucursales";
                response.Result = new List<Office>();
            }
            return response;
        }

        public async Task<Response<string>> CreateOffice(CreateOfficeRequest office)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);

            try
            {
                var res = _db.Offices.Where(x => x.Code == office.Code || x.Identification == office.Identification || x.Description == office.Description || x.Address == office.Address).ToList();

                if (res.Count == 0)
                {
                    var userId = _db.Users.Where(x => x.Username == office.Username).FirstOrDefault().Id;
                    Office newOffice = new Office();
                    newOffice.Code = office.Code;
                    newOffice.Identification = office.Identification;
                    newOffice.Description = office.Description;
                    newOffice.Address = office.Address;
                    newOffice.Currency = office.Currency;
                    newOffice.CreateUser = userId;
                    newOffice.CreateDate = DateTime.Now;


                    _db.Offices.Add(newOffice);
                    _db.SaveChanges();
                    
                    log.Log("Sucursal creada", userId);

                    response.Status = "success";
                    response.Message = "Sucursal creada correctamente.";
                    response.Result = "";
                }
                else
                {
                    response.Status = "info";
                    response.Message = "Ya existe una sucursal con los datos registrados, estos deben ser unicos para cada sucursal.";
                    response.Result = "";
                }


            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ocurrio un error creando la sucursal";
                response.Result = "";
            }
            return response;
        }
    }
}
