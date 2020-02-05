using Swallow.Core.Domains.User;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;

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
            user.IsAccountActive = true;
            _unitOfWork.SaveChanges();
        }

        public void DeactivateUserAccount(Guid userId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user.UserRole.Equals(UserRole.Admin))
            {
                throw new ArgumentException();
            }
            user.IsAccountActive = false;
            _unitOfWork.SaveChanges();
        }

        public ICollection<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAll();
        }
    }
}
