using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PreAceleracion.Entities
{
    public class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }
        [Key]
        public int IdGenero { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
