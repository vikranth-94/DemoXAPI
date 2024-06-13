using InterviewPrepApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewPrepApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        string GenerateJwtToken(User user);
    }
    public class UserService: IUserService
    {
        private readonly AuthContext _context;
    private readonly IConfiguration _configuration;

    public UserService(AuthContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.username == username);

        if (user == null || !VerifyPasswordHash(password, user.password))
        {
            return null;
        }

        return user;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        // Implement password hash verification logic here.
        // For simplicity, we'll assume the stored hash is just the plain password.
        return password == storedHash;
    }
}
}