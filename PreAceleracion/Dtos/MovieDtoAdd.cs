using PreAceleracion.Entities;
using System;
using System.Collections.Generic;

namespace PreAceleracion.Dtos
{
    public class MovieDtoAdd
    {
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; }
        public int Calification { get; set; }
        public int GenreId { get; set; }
        public List<int> CharactersId { get; set; }
    }
}
