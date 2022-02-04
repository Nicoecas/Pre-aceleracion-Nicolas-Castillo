using PreAceleracion.Entities;

namespace PreAceleracion.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
