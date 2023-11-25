using OfficeAdmin_API.Models;
using OfficeAdmin_API.Models.Request;
using OfficeAdmin_API.Models.Response;
using OfficeAdmin_API.Services.Interfaces;

namespace OfficeAdmin_API.Services
{
    public class AdminService: IAdminService
    {
        private readonly TestDBContext _db;
        public AdminService(TestDBContext db)
        {
            _db = db;
        }

        public async Task<Response<UserResponse>> Login(string username, string password)
        {
            Response<UserResponse> response = new Response<UserResponse>();
            LogService log = new LogService(_db);
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
                    if(user.Password == password)
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

        public async Task<Response<string>> CreateUser(CreateUserRequest newUser)
        {
            Response<string> response = new Response<string>();
            LogService log = new LogService(_db);
            try
            {
                var user = _db.Users.Where(x => x.Username == newUser.Username).FirstOrDefault();
                if(user == null)
                {
                    User createUser = new User();
                    createUser.Username = newUser.Username;
                    createUser.Name = newUser.Name;
                    createUser.Lastname = newUser.Lastname;
                    createUser.Password = newUser.Password;
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
            try
            {
                var user = _db.Users.Where(x => x.Username == modifyUser.Username).FirstOrDefault();
                if (user == null)
                {
                    user.Name = modifyUser.Name;
                    user.Lastname = modifyUser.Lastname;
                    user.Password = modifyUser.Password;
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


    }
}
