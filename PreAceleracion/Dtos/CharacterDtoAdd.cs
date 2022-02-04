using PreAceleracion.Entities;
using System.Collections.Generic;

namespace PreAceleracion.Dtos
{
    public class CharacterDtoAdd
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public string History { get; set; }
        public virtual ICollection<CharacterMovie> Movies { get; set; }
    }
}
