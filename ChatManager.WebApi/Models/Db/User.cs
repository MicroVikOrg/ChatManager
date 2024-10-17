using System.Text.Json.Serialization;

namespace ChatManager.WebApi.Models.Db
{
    public partial class User
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; } = null!;
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonPropertyName("verified")]
        public bool? Verified { get; set; }
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}