using Microsoft.AspNetCore.Mvc;
using MySocialMedia.Common.DTOs;
using MySocialMedia.Logic.Services;

namespace MySocialMedia.API.Controllers
{
    [Route("[Controller]")]

    public class ChatsController : Controller
    {
        private readonly IChatsService _chatsService;
        [HttpPost ,Route(nameof(AddMessage))]
        public void AddMessage([FromBody ]MessageDTO p_mess)
        {
            _chatsService.AddMessage(p_mess);
        }
        [HttpPost ,Route(nameof(GetAllByUser))]
        public List<MessageDTO> GetAllByUser([FromBody]long p_userId)
        {
            return _chatsService.GetAllByUser(p_userId);
        }
    }
}
