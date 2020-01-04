using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Swallow.Core.Domains.User;
using Swallow.Core.Repository;

namespace Swallow.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void ActivateUserAccount(Guid userId)
        {
            User user = _unitOfWork.Users.Get(userId);
            user.UserRole.IsAccountActive = true;
            _unitOfWork.SaveChanges();
        }

        public ICollection<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAll();
        }
    }
}
