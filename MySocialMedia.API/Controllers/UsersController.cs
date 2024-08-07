using Microsoft.AspNetCore.Mvc;
using MySocialMedia.Common.DTOs;
using MySocialMedia.Common.ResponseLogin;
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
        public ResponseLogin Login([FromBody]LoginReq p_lr)
        {
            return _userService.Login(p_lr.UserName, p_lr.Password);    
        }
        [HttpPost, Route(nameof(Signin))]
        public void Signin([FromBody]SigninReq p_sr)
        {
             _userService.Signin(p_sr.FirstName , p_sr.LastName , p_sr.UserName , p_sr.Password);
        }
    }
    public class LoginReq
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class SigninReq
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
       
    }
}
