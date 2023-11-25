using OfficeAdmin_API.Models;

namespace OfficeAdmin_API.Services
{
    public class LogService
    {
        private readonly TestDBContext _db;
        public LogService(TestDBContext db)
        {
            _db = db;
        }

        public void Log(string description, int userId)
        {
            try
            {
                Log log = new Log();
                log.Description = description;
                log.UserId = userId;
                log.LogDate = DateTime.Now;

                _db.Logs.Add(log);
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
