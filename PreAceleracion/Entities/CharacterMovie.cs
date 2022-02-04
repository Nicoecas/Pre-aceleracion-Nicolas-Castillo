using System.Text.Json.Serialization;

namespace PreAceleracion.Entities
{
    public class CharacterMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set;}
        public int CharacterId { get; set; }

        [JsonIgnore]
        public virtual Movie movie { get; set; }
        [JsonIgnore]
        public virtual Character character { get; set; }

    }
}
