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

        public UserStatusAppModel GetUserStatus()
        {
            HttpContextAccessor h = new HttpContextAccessor();

            ClaimsIdentity identity = h.HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            return new UserStatusAppModel
            {
                IsLoggedIn = identity.IsAuthenticated,
                Role = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value,
                Email = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value
            };
        }

        public UserAppModel GetConnectedUserData()
        {
            var userStatusAppModel = GetUserStatus();

            if (userStatusAppModel != null)
            {
                var userConnected = GetUserByEmail(userStatusAppModel.Email);

                return new UserAppModel
                {
                    Id = userConnected.IdUser,
                    FirstName = userConnected.GivenName,
                    LastName = userConnected.FamilyName,
                    Email = userConnected.Email,
                    Password = userConnected.Password,
                    Role = userConnected.Role,
                };
            }

            return null;
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

        public List<UserAppModel> GetAllUsers()
        {
            var usersModel = _context.Users.ToList();

            var userAppModel = new List<UserAppModel>();

            foreach (var user in usersModel)
            {
                userAppModel.Add(new UserAppModel
                {
                    Id = user.IdUser,
                    FirstName = user.GivenName,
                    LastName = user.FamilyName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                });
            }

            return userAppModel;
        }

        public void UpdateUser(User userModel)
        {
            if (CheckForUserExistanceById(userModel.IdUser))
            {
                try
                {
                    _context.Users
                        .Where(u => u.IdUser == userModel.IdUser)
                        .ExecuteUpdate(up => up
                            .SetProperty(u => u.FamilyName, userModel.FamilyName)
                            .SetProperty(u => u.GivenName, userModel.GivenName)
                            .SetProperty(u => u.Email, userModel.Email)
                            .SetProperty(u => u.Password, userModel.Password)
                            .SetProperty(u => u.Role, userModel.Role)
                        );
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
            else
            {
                throw new InvalidOperationException("No user found in DB!");
            }
        }

        private bool CheckForUserExistanceById(int id)
        {
            foreach (var user in _context.Users)
            {
                if (user.IdUser == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
