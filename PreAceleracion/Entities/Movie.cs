using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PreAceleracion.Entities
{
    public class Movie
    {
        public Movie()
        {
            Characters = new HashSet<CharacterMovie>();
        }
        [Key]
        public int IdMovie { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; }
        public int Calification { get; set; }
        public int GenreId { get; set; }
        public virtual ICollection<CharacterMovie> Characters { get; set; }
        [JsonIgnore]
        public virtual Genre Genre { get; set; }
    }
}
