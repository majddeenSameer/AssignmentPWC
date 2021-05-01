using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.DomainModels;
using Domain.Interfaces;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;

        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _jwtBearerTokenSettings = jwtTokenOptions.Value;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public virtual async Task<ActionResult<LoginResultDto>> Login(LoginCredentialsDto credentials)
        {
            User user;
            if (credentials == null
                || (user = await ValidateUser(credentials)) == null)
            {
                throw new UnauthorizedAccessException("Login failed");
            }

            var token = GenerateToken(user, user.UserRoles.Where(r => r.Role.ApplicationId == credentials.ApplicationId).Select(ur => ur.Role.Name).ToList());
            return Ok(new { Token = token, Username = user.UserName });
        }

        private async Task<User> GetUser(string username)
        {
            var identityUser = _userManager.Users.Where(u => u.UserName == username)
                .Select(e => new User
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    Email = e.Email,
                    FirstNameEn = e.FirstNameEn,
                    LastNameEn = e.LastNameEn,
                    PhoneNumber = e.PhoneNumber,
                    UserRoles = e.UserRoles.Select(ur => new UserRole { Role = ur.Role }).ToList(),
                    PasswordHash = e.PasswordHash
                }).SingleOrDefault();

            return identityUser;
        }

        private async Task<User> ValidateUser(LoginCredentialsDto credentials)
        {

            var identityUser = await GetUser(credentials.Username);

            if (identityUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                return result == PasswordVerificationResult.Failed ? null : identityUser;
            }

            return null;
        }

        private object GenerateToken(User user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim("UserName", user.UserName.ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim("NameEn",user.FirstNameEn + " " + user.LastNameEn),

                };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddYears(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSettings.Audience,
                Issuer = _jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
