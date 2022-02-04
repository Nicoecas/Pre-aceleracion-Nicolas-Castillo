using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PreAceleracion.Entities
{
    public class Character
    {
        public Character()
        {
            Movies = new HashSet<CharacterMovie>();
        }
        [Key]
        public int IdCharacter { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public string History { get; set; }
        public virtual ICollection<CharacterMovie> Movies { get; set; }
    }
}
