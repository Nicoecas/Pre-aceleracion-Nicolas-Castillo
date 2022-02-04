using System;

namespace PreAceleracion.Dtos
{
    public class UserRegisterDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public UserRegisterDto()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
}
