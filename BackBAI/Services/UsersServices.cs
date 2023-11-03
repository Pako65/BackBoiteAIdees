using BackBAI.Models;
using BackBAI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackBAI.Services
{
    public class UsersServices : Controller
    {
        private readonly ideeContext _context;

        public UsersServices(ideeContext context)
        {
            _context = context;
        }
        // GET : Users
        public IEnumerable<Users> GetUsers()
        {
            var result = _context.Users.ToList();
            return result;
        }
        // POST : Create a new users
        public Users CreateUsers(Users users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();

            return users;
        }
        // PUT : Modify a user
        public bool PutUsers(int id, UserDTO usersDTO)
        {
            var putUsers = _context.Users.Find(id);

            if (putUsers == null)
            {
                return false;
            }

            if (putUsers.Email != null)
                putUsers.Email = usersDTO.Email;

            //if (putUsers.Password != null)
            //    putUsers.Password = usersDTO.Password;

            _context.SaveChanges();

            return true;
        }
        // DELETE : Delete one user by id
        public bool DeleteUsers(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}