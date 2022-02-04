using System;

namespace PreAceleracion.Dtos
{
    public class MovieDtoPut
    {
        public int IdMovie { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; }
        public int Calification { get; set; }
    }
}
