using ImHere.API.Helpers;
using ImHere.Business.Abstract;
using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using ImHere.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace ImHere.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly AppSettings _appSettings;


        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;

        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest model)
        {
            User user = await this.GetUserByEmail(model.email);
            if (user == null) 
                return null;

            bool isVerifiedPassword = BC.Verify(model.password, user.password);
            if (!isVerifiedPassword)
                return null;
            

            // Authentication başarılı
            var token = generateJwtToken(user);
            var response = new AuthenticationResponse(user, token);

            return response;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", user.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task CreateUser(User user)
        {
            user.role = 0;
            user.password = BC.HashPassword(user.password);
            user.no = user.no.ToLower();
            await _userRepository.CreateUser(user);
        }

        public async Task<UserInfoDto> GetUserInfoById(int id)
        {
            return await _userRepository.GetUserInfoById(id);
        }

        public async Task<User> GetUserByNo(string no)
        {
            return await _userRepository.GetUserByNo(no.ToLower());
        }
    }
}
