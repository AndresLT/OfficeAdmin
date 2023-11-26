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

        public async Task<Response<List<Office>>> GetOffices(bool all)
        {
            Response<List<Office>> response = new Response<List<Office>>();
            try
            {
                var res = new List<Office>();
                if(all)
                {
                    res = _db.Offices.ToList();
                }
                else
                {
                    res = _db.Offices.Where(x => x.Active).ToList();
                }

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
                    newOffice.Identification = office.Identification.ToUpper();
                    newOffice.Description = office.Description;
                    newOffice.Address = office.Address;
                    newOffice.Currency = office.Currency;
                    newOffice.CreateUser = userId;
                    newOffice.CreateDate = DateTime.Now;
                    newOffice.Active = true;


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

        public async Task<Response<string>> ModifyOffice(ModifyOfficeRequest office)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);

            try
            {
                var res = _db.Offices.Where(x => x.Id != office.id && (x.Code == office.Code || x.Identification == office.Identification || x.Description == office.Description || x.Address == office.Address)).ToList();

                if(res.Count > 0)
                {
                    response.Status = "warning";
                    response.Message = "Ya existen sucursales creadas con los datos ingresados, por favor verificar los campos de Codigo, Identificacion, Descripcion o Direccion.";
                    response.Result = "";
                }
                else
                {
                    var officeRes = _db.Offices.Where(x => x.Id == office.id).FirstOrDefault();

                    if (officeRes != null)
                    {
                        if (officeRes.Code == office.Code &&
                            officeRes.Identification == office.Identification &&
                            officeRes.Description == office.Description &&
                            officeRes.Address == office.Address &&
                            officeRes.Currency == office.Currency &&
                            officeRes.Active == office.Active)
                        {
                            response.Status = "info";
                            response.Message = "No hay datos para modificar.";
                            response.Result = "";
                        }
                        else
                        {
                            var userId = _db.Users.Where(x => x.Username == office.Username).FirstOrDefault().Id;
                            officeRes.Code = office.Code;
                            officeRes.Identification = office.Identification.ToUpper();
                            officeRes.Description = office.Description;
                            officeRes.Address = office.Address;
                            officeRes.Currency = office.Currency;
                            officeRes.ModifyUser = userId;
                            officeRes.ModifyDate = DateTime.Now;
                            officeRes.Active = office.Active;


                            _db.Offices.Update(officeRes);
                            _db.SaveChanges();

                            log.Log("Sucursal con ID: " + officeRes.Id + " modificada.", userId);

                            response.Status = "success";
                            response.Message = "Sucursal editada correctamente.";
                            response.Result = "";
                        }

                    }
                    else
                    {
                        response.Status = "info";
                        response.Message = "La sucursal no fue encontrada.";
                        response.Result = "";
                    }
                }
                


            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ocurrio un error modificando la sucursal";
                response.Result = "";
            }
            return response;
        }
    }
}
