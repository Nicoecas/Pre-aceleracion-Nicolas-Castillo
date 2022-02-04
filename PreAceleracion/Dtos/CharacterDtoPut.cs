using PreAceleracion.Entities;
using System.Collections.Generic;

namespace PreAceleracion.Dtos
{
    public class CharacterDtoPut
    {
        public int IdCharacter { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public string History { get; set; }
    }
}
