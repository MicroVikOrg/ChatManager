using System.Text.Json.Serialization;

namespace ChatManager.WebApi.Models.Db
{
    public partial class Chat
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("chatname")]
        public string Chatname { get; set; } = null!;
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}