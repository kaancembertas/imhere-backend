// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.API.Helpers;
using ImHere.Business.Abstract;
using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using ImHere.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace ImHere.Business.Concrete
{
    public class AuthenticationService:IAuthenticationService
    {
        IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUserRepository userRepository,
            IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest model)
        {
            User user = await _userRepository.GetUserByEmail(model.email);
            if (user == null)
                return null;

            bool isVerifiedPassword = BC.Verify(model.password, user.password);
            if (!isVerifiedPassword)
                return null;


            // Authentication başarılı
            DateTime expireDate = DateTime.UtcNow.AddDays(14);
            var token = GenerateJwtToken(user, expireDate);
            var response = new AuthenticationResponse(token, expireDate);

            return response;
        }

        private string GenerateJwtToken(User user, DateTime expireDate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", user.id.ToString())
                }),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
