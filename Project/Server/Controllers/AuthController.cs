using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Entities.Models.Auth;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser>signInManager, 
        IConfiguration configuration) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager=userManager;
        private readonly RoleManager<IdentityRole> _roleManager=roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager=signInManager;
        private readonly IConfiguration _configuration=configuration;
        
        
[HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody]Register entity,[FromQuery] string role)
        {
            ApplicationUser user = 
                new ApplicationUser{UserName = entity.Name, Email = entity.Email};
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var roleCreationgResult = await _roleManager.CreateAsync(new IdentityRole(entity.Role));
                if (!roleCreationgResult.Succeeded) return BadRequest("Failed to create role");
                
            }
            
            var result = await _userManager.CreateAsync(user, entity.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(user, role);
            return Ok("User registered successfully");
        }
[HttpPost("[action]")]
        public async Task<ActionResult> Login([FromBody]Login entity)
        {
            var user = await _userManager.FindByEmailAsync(entity.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, entity.Password)) 
                return Unauthorized("Invalid credentials");
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            authClaims.AddRange(userRoles.Select(r => 
                new Claim(ClaimTypes.Role, r)));
            var token = GenerateJwtToken(authClaims);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["Key"]));
            var token = new JwtSecurityToken(
                issuer: jwtSetting["Issuer"],
                audience:jwtSetting["Audience"],
                
                expires:DateTime.Now.AddHours(3),
                claims:claims,
                signingCredentials:new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
