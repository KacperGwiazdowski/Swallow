using System;
using System.Collections.Generic;
using System.Security.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swallow.Core.Domains.User;
using Swallow.Core.Services;
using Swallow.WebApi.Models;

namespace Swallow.WebApi.Controllers
{
    [ApiController]
    [Authorize(Policy = "RequireAdmin")]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IAdminService _adminService;

        public AdminController(ILogger<UserController> logger, IMapper mapper, IAdminService adminService)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _adminService = adminService ??
                throw new ArgumentNullException(nameof(adminService));
        }

        [HttpPut(nameof(ActivateUserAccount) + "/{id}")]
        public ActionResult ActivateUserAccount(Guid id)
        {
            _adminService.ActivateUserAccount(id);
            return Ok();
        }

        [HttpPut(nameof(DectivateUserAccount) + "/{id}")]
        public ActionResult DectivateUserAccount(Guid id)
        {
            _adminService.ActivateUserAccount(id);
            return Ok();
        }

        [HttpGet(nameof(GetAllUsers))]
        public ActionResult<ICollection<UserDto>> GetAllUsers()
        {
            var userToMap = _adminService.GetAllUsers();
            var usersToReturn = _mapper.Map<ICollection<UserDto>>(userToMap);
            return Ok(usersToReturn);
        }
    }
}
