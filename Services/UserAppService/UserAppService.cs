using CinemaVillage.AppModel.Users;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.UserAppService.Interface;

namespace CinemaVillage.Services.UserAppService
{
    public class UserAppService : IUserAppService
    {
        private readonly CinemaDbContext _context;

        public UserAppService(CinemaDbContext context) 
        { 
            _context = context;
        }

        public void AddUser(User userModel)
        {
            if (!CheckForUserExistance(userModel.Email))
            {
                try
                {
                    _context.Users.Add(userModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User already exists!");
            }
        }

        public bool CheckForUserExistance(string email)
        {
            foreach (var user in _context.Users)
            {
                if (user.Email.Equals(email))
                {
                    return true;
                }
            }

            return false;
        }

        public User GetUserByEmail (string email) 
        { 
            User user = _context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            return user;
        }
    }
}
