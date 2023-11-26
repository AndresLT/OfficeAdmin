using OfficeAdmin_API.Models;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;
using OfficeAdmin_API.Services.Interfaces;

namespace OfficeAdmin_API.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly TestDBContext _db;
        public CurrencyService(TestDBContext db)
        {
            _db = db;
        }
        public async Task<Response<List<Currency>>> GetCurrencies(bool all)
        {
            Response<List<Currency>> response = new Response<List<Currency>>();
            try
            {
                var res = new List<Currency>();
                if (all)
                {
                    res = _db.Currencies.ToList();
                }
                else
                {
                    res = _db.Currencies.Where(x => x.Active).ToList();
                }

                if (res.Count == 0)
                {
                    response.Status = "info";
                    response.Message = "No se encontraron monedas.";
                    response.Result = res;
                }
                else
                {
                    response.Status = "success";
                    response.Message = "Monedas consultadas correctamente";
                    response.Result = res;
                }


            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ocurrio un error consultando sucursales";
                response.Result = new List<Currency>();
            }
            return response;
        }

        public async Task<Response<string>> CreateCurrency(CreateCurrencyRequest currency)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);

            try
            {
                var res = _db.Currencies.Where(x => x.Code == currency.Code || x.Description == currency.Description).ToList();

                if (res.Count == 0)
                {
                    var userId = _db.Users.Where(x => x.Username == currency.Username).FirstOrDefault().Id;
                    Currency newCurrency = new Currency();
                    newCurrency.Code = currency.Code.ToUpper();
                    newCurrency.Description = currency.Description;
                    newCurrency.Active = true;


                    _db.Currencies.Add(newCurrency);
                    _db.SaveChanges();

                    log.Log("Moneda creada", userId);

                    response.Status = "success";
                    response.Message = "Moneda creada correctamente.";
                    response.Result = "";
                }
                else
                {
                    response.Status = "info";
                    response.Message = "Ya existe una o mas monedas con los datos registrados, estos deben ser unicos para cada moneda.";
                    response.Result = "";
                }


            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ocurrio un error creando la moneda";
                response.Result = "";
            }
            return response;
        }

        public async Task<Response<string>> ModifyCurrency(ModifyCurrencyRequest currency)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);

            try
            {
                var res = _db.Currencies.Where(x => x.Id != currency.id && (x.Code == currency.Code || x.Description == currency.Description)).ToList();

                if (res.Count > 0)
                {
                    response.Status = "warning";
                    response.Message = "Ya existen monedas creadas con los datos ingresados, por favor verificar los campos de Codigo y Descripcion.";
                    response.Result = "";
                }
                else
                {
                    var currencyRes = _db.Currencies.Where(x => x.Id == currency.id).FirstOrDefault();

                    if (currencyRes != null)
                    {
                        if (currencyRes.Code == currency.Code &&
                            currencyRes.Description == currency.Description &&
                            currencyRes.Active == currency.Active)
                        {
                            response.Status = "info";
                            response.Message = "No hay datos para modificar.";
                            response.Result = "";
                        }
                        else
                        {
                            var userId = _db.Users.Where(x => x.Username == currency.Username).FirstOrDefault().Id;
                            currencyRes.Code = currency.Code;
                            currencyRes.Description = currency.Description;
                            currencyRes.Active = currency.Active;


                            _db.Currencies.Update(currencyRes);
                            _db.SaveChanges();

                            log.Log("Moneda con ID: " + currencyRes.Id + " modificada.", userId);

                            response.Status = "success";
                            response.Message = "Moneda editada correctamente.";
                            response.Result = "";
                        }

                    }
                    else
                    {
                        response.Status = "info";
                        response.Message = "La moneda no fue encontrada.";
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
