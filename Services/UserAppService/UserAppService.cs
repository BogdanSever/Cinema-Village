using CinemaVillage.AppModel.Users;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.UserAppService.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public User GetUserByEmail(string email)
        {
            User user = _context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            return user;
        }

        public LoggedInUserAppModel GetUserStatus()
        {
            HttpContextAccessor h = new HttpContextAccessor();

            ClaimsIdentity identity = h.HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            return new LoggedInUserAppModel
            {
                IsLoggedIn = identity.IsAuthenticated,
                Role = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value,
                Email = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value
            };
        }

        public void DeleteUser(string email)
        {
            if (CheckForUserExistance(email))
            {
                int noOfRowsDeleted = _context.Users.Where(u => u.Email.Equals(email)).ExecuteDelete();
                
                if (noOfRowsDeleted == 0) 
                {
                    throw new InvalidOperationException("There are no rows deleted, even though there was found a user");
                }
            }
            else
            {
                throw new InvalidOperationException("No user found in DB!");
            }
        }
    }
}
