using Microsoft.AspNetCore.Mvc;
using MySocialMedia.Common.DTOs;
using MySocialMedia.Common.ResponseLogin;
using MySocialMedia.Logic.Services;
using MySocialMedia.Common.ModelReq;
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
        public UserSessionDTO Login([FromBody] LoginReq p_lr)
        {
            return _userService.Login(p_lr.UserName, p_lr.Password);
        }
        [HttpPost, Route(nameof(Signin))]
        public void Signin([FromBody] SigninReq p_sr)
        {
            _userService.Signin(p_sr.FirstName, p_sr.LastName, p_sr.UserName, p_sr.Password);
        }
    }


}
