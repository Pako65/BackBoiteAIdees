using BackBAI.Models;
using BackBAI.Models.DTO;
using BackBAI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackBAI.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private ideeContext _context;
        private UsersServices _usersServices;

        public UsersController(ideeContext context, UsersServices usersServices)
        {
            _context = context;
            _usersServices = usersServices;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult Get()
        {
            var users = _usersServices.GetUsers();
            return Ok(users);
        }
        [HttpPost("CreateNewUsers")]
        public IActionResult Post(WebhookDto webhookDto)
        {

            string email = webhookDto.record?.email;
            string encryptedPassword = webhookDto.record?.encrypted_password;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(encryptedPassword))
            {
                var user = new Users
                {
                    Email = email,
                    Password = encryptedPassword
                };

                _context.Users.Add(user);
                _context.SaveChanges(); 
            }
            return Ok("Données email et encrypted_password ajoutées en base de données avec succès.");
        }
        [HttpDelete("{id}/DeleteUsersById")]
        public IActionResult DeleteUsersById(int id)
        {
            var resultDeleteUser = _usersServices.DeleteUsers(id);

            if(resultDeleteUser == false)
            {
                return NotFound();
            }
            return Ok(resultDeleteUser);
        }
        [HttpPut("{id}/PutUsers")]
        public IActionResult PutUsersById(int id, [FromBody] Users users)
        {
            var resultPut = _usersServices.PutUsers(id, users);

            if(!resultPut)
            {
                return NotFound();
            }

            return Ok(resultPut);
        }
    }
}
