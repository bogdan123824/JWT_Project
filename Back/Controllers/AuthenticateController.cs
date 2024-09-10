using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Back.Model;
using Back.Responses;


namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(
        IConfiguration configuration,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] ResponseLog model)
    {
        var user = await _userManager
            .FindByNameAsync(model.Username);

        if (user is null)
        {
            return Unauthorized("User with given username does not exist.");
        }

        var passwordIsCorrect = await _userManager
            .CheckPasswordAsync(user, model.Password);

        if (!passwordIsCorrect)
        {
            return Unauthorized("Password is incorrect.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = new JwtSecurityTokenHandler()
            .WriteToken(GetToken(authClaims));

        var loginResponse = new ResponseTok
        {
            Token = token,
            Role = userRoles.Count() == 2 ? UserRoles.Admin : UserRoles.User,
        };

        return Ok(loginResponse);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] ResponseReg model)
    {
        var existingUser = await _userManager
            .FindByNameAsync(model.Username);

        if (existingUser is not null)
        {
            return Unauthorized("User already exists!");
        }

        IdentityUser user = new()
        {
            Email = model.Email,
            UserName = model.Username,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager
            .CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return Unauthorized("Please check user credentials.");
        }

        return Ok("User was created successfully!");
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] ResponseReg model)
    {
        var existingUser = await _userManager
            .FindByNameAsync(model.Username);

        if (existingUser is not null)
        {
            return Unauthorized("User already exists!");
        }

        IdentityUser user = new()
        {
            Email = model.Email,
            UserName = model.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager
            .CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return Unauthorized("Please check user credentials.");
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        await _userManager.AddToRoleAsync(user, UserRoles.User);
        await _userManager.AddToRoleAsync(user, UserRoles.Admin);

        return Ok("User was created successfully!");
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(24),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}
