using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swallow.Core.Domains.User;
using Swallow.Core.Repository;

namespace Swallow.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordSecurityService _passwordSecurityService;

        public UserService(IUnitOfWork unitOfWork, IPasswordSecurityService passwordSecurityService)
        {
            //_configuration = configuration ??
            //    throw new ArgumentNullException(nameof(configuration));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _passwordSecurityService = passwordSecurityService ??
                throw new ArgumentNullException(nameof(passwordSecurityService));
        }

        public Guid AddUser(User user)
        {
            user.UserRole = UserRole.NormalUser;
            user.PasswordHash = _passwordSecurityService.HashPassword(user.PasswordHash);
            var guid = _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();
            return guid;
        }

        public User GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<User, string> SignInAsync(string username, string password)
        {
            var user = _unitOfWork.Users.GetByUsername(username);
            if (!user.IsAccountActive)
            {
                throw new InvalidCredentialException("Account is not activated yet");
            }
            var isPasswordValid = _passwordSecurityService.ValidatePassword(password, user.PasswordHash);
            var key = Encoding.ASCII.GetBytes("abcdeefasdfafsdfgaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            if (isPasswordValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity
                    (
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.FullName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Role, user.UserRole.ToString())
                        }
                    ),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new KeyValuePair<User, string>(user, tokenHandler.WriteToken(token));
            }
            else
            {
                throw new InvalidCredentialException("Password is not valid");
            }
        }
    }
}
