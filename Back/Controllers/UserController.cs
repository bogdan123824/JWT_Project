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
            var existingUsers = await _userManager.Users.ToListAsync();
            var usersToSend = new List<object>();

            foreach (var user in existingUsers)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var userInfo = new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Role = userRoles.Contains(UserRoles.Admin) ? UserRoles.Admin : UserRoles.User
                };

                usersToSend.Add(userInfo);
            }

            return Ok(usersToSend); 
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> EditUser([FromRoute] string id, [FromBody] EditUserModel model)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(); 
            }

            existingUser.UserName = model.Username; 

            if (model.Role == UserRoles.Admin)
            {
                await _userManager.AddToRoleAsync(existingUser, UserRoles.Admin);
            }
            else if (model.Role == UserRoles.User)
            {
                await _userManager.RemoveFromRoleAsync(existingUser, UserRoles.Admin);
            }

            return Ok(); 
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(); 
            }

            await _userManager.DeleteAsync(existingUser); 

            return Ok(); 
        }
    }
}