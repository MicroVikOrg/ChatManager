using ChatManager.WebApi.Interfaces;
using ChatManager.WebApi.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatManager.WebApi.Controllers
{
    [Route("api/chats/members")]
    [ApiController]
    public class ChatMembersController : ControllerBase
    {
        private readonly IKafkaProducer _kafkaProducer;
        private ApplicationContext db;
        public ChatMembersController(IKafkaProducer kafkaProducer, ApplicationContext context)
        {
            _kafkaProducer = kafkaProducer;
            db = context;
        }
        [HttpPost]
        async public Task<IActionResult> AddChatMember(string userId, string chatId)
        {
            var user = await db.Users.FirstOrDefaultAsync(e => e.Id == Guid.Parse(userId));
            var chat = await db.Chats.FirstOrDefaultAsync(e => e.Id == Guid.Parse(chatId));
            if (user == null || chat == null) return BadRequest();
            user.Chats.Add(chat);
            chat.Users.Add(user);

            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        async public Task<IActionResult> DeleteChatMember(string userId, string chatId)
        {
            var user = await db.Users.FirstOrDefaultAsync(e => e.Id == Guid.Parse(userId));
            var chat = await db.Chats.FirstOrDefaultAsync(e => e.Id == Guid.Parse(chatId));
            if (user == null || chat == null) return BadRequest();
            user.Chats.Remove(chat);
            chat.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
