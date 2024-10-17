using ChatManager.WebApi.Interfaces;
using ChatManager.WebApi.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ChatManager.WebApi.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IKafkaProducer _kafkaProducer;
        private ApplicationContext db;
        public ChatsController(IConfiguration configuration, IKafkaProducer kafkaProducer, ApplicationContext context)
        {
            _configuration = configuration;
            _kafkaProducer = kafkaProducer;
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetChatsByUser(string userId)
        {
            var user = await db.Users.FindAsync(Guid.Parse(userId));
            if (user == null) return BadRequest();
            return Ok(user.Chats.Select(e => e.Id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] Chat chat)
        {
            chat.Id = Guid.NewGuid();
            await db.Chats.AddAsync(chat);
            await db.SaveChangesAsync();
            await _kafkaProducer.ProduceMessage("NewChats", JsonConvert.SerializeObject(chat));
            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteChat(string chatId)
        {
            var chat = await db.Chats.FindAsync(Guid.Parse(chatId));
            if (chat == null) return NotFound();
            db.Chats.Remove(chat);
            await db.SaveChangesAsync();
            return Ok($"chat {chat.Chatname} was succefully deleted");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateChat([FromBody] Chat newChat)
        {
            var chat = await db.Chats.FindAsync(newChat.Id);
            if (chat == null) return BadRequest();
            chat.Chatname = newChat.Chatname;
            chat.UpdatedAt = DateTime.UtcNow;
            db.Chats.Update(chat);
            await db.SaveChangesAsync();
            return Ok(chat);
        }
    }
}