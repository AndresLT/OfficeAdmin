using OfficeAdmin_API.Models;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;
using OfficeAdmin_API.Services.Interfaces;
using System.Text;

namespace OfficeAdmin_API.Services
{
    public class AdminService: IAdminService
    {
        private readonly TestDBContext _db;
        private readonly IConfiguration _config;

        public AdminService(TestDBContext db, IConfiguration config)
        {
            _config = config;
            _db = db;
        }

        public async Task<Response<UserResponse>> Login(string username, string password)
        {
            Response<UserResponse> response = new Response<UserResponse>();
            LogService log = new LogService(_db);
            EncryptService encryptService = new EncryptService(_config["Key"]);
            try
            {
                var user = _db.Users.Where(x => x.Username == username).FirstOrDefault();
                if(user == null)
                {
                    response.Status = "warning";
                    response.Message = "Credenciales incorectas. Por favor vuelve a intentarlo.";
                    response.Result = new UserResponse();
                    return response;
                }

                
                if(user.Attemps > 0 || user.Active == true)
                {
                    if(encryptService.DecryptAsync(user.Password).Result == password)
                    {
                        user.Attemps = 3;

                        _db.Update(user);
                        _db.SaveChanges();

                        log.Log("Inicio de sesion correcto.", user.Id);

                        response.Status = "success";
                        response.Message = "Credenciales correctas. Bienvenido/a.";
                        response.Result = new UserResponse { username = username, name = user.Name, lastname = user.Lastname };
                        return response;
                    }
                    else
                    {
                        user.Attemps -= 1;
                        if(user.Attemps <= 0)
                        {
                            user.Active = false;
                            _db.Update(user);
                            _db.SaveChanges();

                            log.Log("Inicio de sesion incorrecto. Bloqueo de cuenta", user.Id);


                            response.Status = "warning"; 
                            response.Message = "Te quedan " + user.Attemps + " intentos. Tu cuenta ha sido bloquedada. Comunicate con el administrador para desbloquearla";
                            response.Result = new UserResponse();
                            return response;
                        }
                        else
                        {
                            _db.Update(user);
                            _db.SaveChanges();

                            log.Log("Inicio de sesion incorrecto.", user.Id);


                            response.Status = "warning";
                            response.Message = "Credenciales incorectas. Por favor vuelve a intentarlo. Te quedan " + user.Attemps + " intentos antes de bloquear tu cuenta.";
                            response.Result = new UserResponse();
                            return response;
                        }
                        
                    }
                }
                else
                {
                    log.Log("Intento de inicio de sesion en cuenta bloqeuada o inactiva.", user.Id);


                    response.Status = "warning";
                    response.Message = "Tu cuenta esta bloqueada o inactiva, comunicate con el administrador para desbloquearla.";
                    response.Result = new UserResponse();
                    return response;
                }

            }catch(Exception ex)
            {
                response.Status = "error";
                response.Message = "Ha ocurrido un error inesperado. "+ ex.Message;
                response.Result = new UserResponse();
                return response;
            }
        }

        public async Task<Response<string>> Logout(string username)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);
            try
            {
                var user = _db.Users.Where(x => x.Username == username).FirstOrDefault();

                log.Log("Cierre de sesion", user.Id);

                response.Status = "success";
                response.Message = "Cierre de sesion correcto.";
                response.Result = "";

            }
            catch
            {

                response.Status = "error";
                response.Message = "Ocurrio un error inesperado cerrando sesion";
                response.Result = "";
            }
            return response;
        }

        public async Task<Response<List<UserAdminResponse>>> GetUsers()
        {
            Response<List<UserAdminResponse>> response = new Response<List<UserAdminResponse>>();

            try
            {
                var res = _db.Users.Select(x => new UserAdminResponse { Id = x.Id, Username = x.Username, Name = x.Name, Lastname = x.Lastname, Active = x.Active }).ToList();

                if (res.Count > 0)
                {
                    response.Status = "success";
                    response.Message = "Usuarios consultados correctamente.";
                    response.Result = res;
                }
                else
                {
                    response.Status = "info";
                    response.Message = "No se encontraron usuarios.";
                    response.Result = new List<UserAdminResponse>();
                }


                return response;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ha ocurrido un error inesperado. " + ex.Message;
                response.Result = new List<UserAdminResponse>();
                return response;
            }
        }

        public async Task<Response<string>> CreateUser(CreateUserRequest newUser)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);
            EncryptService encryptService = new EncryptService(_config["Key"]);
            try
            {
                var user = _db.Users.Where(x => x.Username == newUser.Username).FirstOrDefault();
                if(user == null)
                {
                    User createUser = new User();
                    createUser.Username = newUser.Username;
                    createUser.Name = newUser.Name;
                    createUser.Lastname = newUser.Lastname;
                    createUser.Password = encryptService.EncryptAsync(newUser.Password).Result;
                    createUser.Active = true;
                    createUser.Attemps = 3;

                    _db.Users.Add(createUser);
                    _db.SaveChanges();

                    response.Status = "success";
                    response.Message = "Usuario registrado correctamente";
                    response.Result = "";

                }
                else
                {
                    response.Status = "info";
                    response.Message = "El usuario ya existe en el sistema";
                    response.Result = "";
                }
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ha ocurrido un error inesperado creando el usuario. " + ex.Message;
                response.Result = "";
            }
            return response;
        }

        public async Task<Response<string>> ModifyUser(ModifyUserRequest modifyUser)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);
            EncryptService encryptService = new EncryptService(_config["Key"]);
            try
            {
                var user = _db.Users.Where(x => x.Username == modifyUser.Username).FirstOrDefault();
                if (user != null)
                {
                    user.Name = modifyUser.Name;
                    user.Lastname = modifyUser.Lastname;
                    user.Active = modifyUser.Active;
                    user.Attemps = 3;

                    _db.Users.Update(user);
                    _db.SaveChanges();

                    response.Status = "success";
                    response.Message = "Usuario actualizado correctamente";
                    response.Result = "";

                }
                else
                {
                    response.Status = "info";
                    response.Message = "El usuario no existe en el sistema";
                    response.Result = "";
                }
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ha ocurrido un error inesperado modificando el usuario. " + ex.Message;
                response.Result = "";
            }
            return response;
        }

        public async Task<Response<string>> ChangeUserPassword(ChangeUserPasswordRequest req)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);
            EncryptService encryptService = new EncryptService(_config["Key"]);
            try
            {
                var user = _db.Users.Where(x => x.Username == req.Username).FirstOrDefault();
                if (user != null)
                {
                    if (user.Active == true)
                    {
                        if (encryptService.DecryptAsync(user.Password).Result != req.Password)
                        {
                            user.Password = encryptService.EncryptAsync(req.Password).Result;

                            _db.Users.Update(user);
                            _db.SaveChanges();

                            log.Log("Cambio de contraseña", user.Id);

                            response.Status = "success";
                            response.Message = "Contraseña actualizada correctamente. Inicia sesion con tu nueva contraseña.";
                            response.Result = "";
                        }
                        else
                        {

                            response.Status = "info";
                            response.Message = "La contraseña no puede ser igual a la anterior.";
                            response.Result = "";

                        }
                    }
                    else
                    {
                        response.Status = "warning";
                        response.Message = "El usuario se encuentra bloqueado, por favor solicitar desbloqueo para realizar el cambio.";
                        response.Result = "";
                    }



                    
                    
                }
                else
                {
                    response.Status = "info";
                    response.Message = "El usuario no existe en el sistema";
                    response.Result = "";
                }
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ha ocurrido un error inesperado actualizando la contraseña. " + ex.Message;
                response.Result = "";
            }
            return response;
        }

        public async Task<Response<List<LogResponse>>> GetLogs()
        {
            Response<List<LogResponse>> response = new Response<List<LogResponse>>();

            try
            {
                var users = _db.Users.Select(x => new User { Id = x.Id, Username = x.Username }).ToList();
                var logs = _db.Logs.Select(x => new Log{ Id = x.Id, Description = x.Description, LogDate = x.LogDate, UserId = x.UserId }).ToList();

                var logsResponse = new List<LogResponse>();
                foreach (var log in logs)
                {
                    var newLog = new LogResponse();
                    newLog.Id = log.Id;
                    newLog.Description = log.Description;
                    newLog.LogDate = log.LogDate;
                    newLog.Username = users.Where(x => x.Id == log.UserId).FirstOrDefault().Username;
                    logsResponse.Add(newLog);
                }

                if (logs.Count > 0)
                {
                    response.Status = "success";
                    response.Message = "Logs consultados correctamente.";
                    response.Result = logsResponse.OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    response.Status = "info";
                    response.Message = "No se encontraron logs.";
                    response.Result = new List<LogResponse>();
                }


                return response;
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Message = "Ha ocurrido un error inesperado. " + ex.Message;
                response.Result = new List<LogResponse>();
                return response;
            }
        }
    }
}
