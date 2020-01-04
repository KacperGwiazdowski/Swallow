using System;
using System.Collections.Generic;
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
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,IMapper mapper, IUserService userService)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }
        
        [AllowAnonymous]
        [HttpPost(nameof(AddNewUser))]
        public ActionResult<Guid> AddNewUser(CreateUserDto createUserDto)
        {
            var userToAdd = _mapper.Map<User>(createUserDto);
            return Ok(_userService.AddUser(userToAdd));
        }

        [AllowAnonymous]
        [HttpPost(nameof(SignIn))]
        public ActionResult<UserLoginDto> SignIn(CredentialsDto credentialsDto)
        {

            var userWithToken = 
                _userService.SignInAsync(credentialsDto.Username, credentialsDto.Password);
            var userToReturn = _mapper.Map<UserLoginDto>(userWithToken.Key);
            userToReturn.Token = userWithToken.Value;
            return Ok(userToReturn);
        }

        [Authorize]
        [HttpGet(nameof(IsLoggedIn))]
        public ActionResult IsLoggedIn()
        {
            return Ok();
        }


        [Authorize]
        [HttpGet(nameof(GetAllUsers))]
        public ActionResult<ICollection<User>> GetAllUsers()
        {
            var usersToMap = _userService.GetAllUsers();
            var usersToReturn = _mapper.Map<ICollection<UserDto>>(usersToMap);
            return Ok(usersToReturn);
        }

    }
}
