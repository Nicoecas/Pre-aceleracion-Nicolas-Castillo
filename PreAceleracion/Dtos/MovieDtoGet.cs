using System;

namespace PreAceleracion.Dtos
{
    public class MovieDtoGet
    {
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; }
    }
}
