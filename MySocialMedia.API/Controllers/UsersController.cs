using Microsoft.AspNetCore.Mvc;
using MySocialMedia.Common.DTOs;
using MySocialMedia.Logic.Services;

namespace MySocialMedia.API.Controllers
{
    [Route("[Controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpPost, Route(nameof(Login))]
        public UserSessionDTO Login([FromBody]LoginReq p_lr)
        {
            return _userService.Login(p_lr.UserName, p_lr.Password);    
        }
    }
    public class LoginReq
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
