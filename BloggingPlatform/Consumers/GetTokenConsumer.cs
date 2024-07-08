using BloggingPlatform.Data;
using BloggingPlatform.Exceptions;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BloggingPlatform.Consumers;

public class GetTokenConsumer : MediatorRequestHandler<GetToken, BearerToken>
{
    private readonly BloggingPlatformContext _context;
    private readonly IConfiguration _configuration;
    public GetTokenConsumer(BloggingPlatformContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    protected override async Task<BearerToken> Handle(GetToken request, CancellationToken cancellationToken)
    {

        var user = _context.Users.SingleOrDefault(u => u.Username == request.Username && u.Password == request.Password);

        if (user == null)
            return new BearerToken();

        var jwtSettings = _configuration.GetSection("Jwt");
        var secretKey = jwtSettings["Key"]!;
        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, issuer),
            new Claim(JwtRegisteredClaimNames.Aud, audience)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds);


        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return await Task.FromResult(new BearerToken { Token = jwtToken });
    }
}
