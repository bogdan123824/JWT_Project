using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Back.Model
{
    [Authorize(Roles = UserRoles.Admin)]  
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersGet = new List<object>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var info = new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Role = userRoles.Contains(UserRoles.Admin) ? UserRoles.Admin : UserRoles.User
                };

                usersGet.Add(info);
            }

            return Ok(usersGet); 
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> EditUser([FromRoute] string id, [FromBody] EditUserModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); 
            }

            user.UserName = model.Username; 

            if (model.Role == UserRoles.Admin)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            else if (model.Role == UserRoles.User)
            {
                await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin);
            }

            return Ok(); 
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); 
            }

            await _userManager.DeleteAsync(user); 

            return Ok(); 
        }
    }
}