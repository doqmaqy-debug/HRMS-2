using HRMS_2.DBcontexts;
using HRMS_2.Dtos.Auth;
using HRMS_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HRMS_2Context _dbcontext;

        public AuthController(HRMS_2Context dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpPost("Login")]
        public IActionResult Login( [FromBody] LoginDto loginDto)
        {
            try
            {
                var user = _dbcontext.Users.FirstOrDefault(x => x.UserName.ToUpper().Contains(loginDto.UserName.ToLower()));

                if (user==null)
                {
                    return BadRequest("Invalid UserName Or Password " );
                }
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.HashedPassword)) 
                {
                    return BadRequest("Invalid UserName Or Password ");
                }

                var Token = GenerateJWTToken(user);

                return Ok(Token);
            }
            catch (Exception ex) 
            
            {
                return BadRequest(ex.Message);
            }
            
            
        }
        private string GenerateJWTToken(User user)

        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));


            if (user.IsAdmid)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            }
            else 
            {
                var employee = _dbcontext.Employees.Include(x => x.Lookup).FirstOrDefault(x=>x.UserId==user.Id);
                claims.Add(new Claim(ClaimTypes.Role, employee.Lookup.Name));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WHAFWEI#!@S!!112312WQEQW@RWQEQW432"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var tokenSettings = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddDays(1)
                 );
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.WriteToken(tokenSettings);
            return token;
        }
    }
}
