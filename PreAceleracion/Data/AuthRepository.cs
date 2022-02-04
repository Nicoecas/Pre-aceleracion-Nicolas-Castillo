using Microsoft.EntityFrameworkCore;
using PreAceleracion.Data.Interfaces;
using PreAceleracion.Entities;
using System.Threading.Tasks;

namespace PreAceleracion.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly PreAceleracionContext preAceleracionContext;
        public AuthRepository(PreAceleracionContext preAceleracionContext)
        {
            this.preAceleracionContext = preAceleracionContext;
        }
        public async Task<bool> ExistUser(string email)
        {
            if (await preAceleracionContext.Users.AnyAsync(x => x.EmailAddress == email))
            {
                return true;
            }
            return false;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await preAceleracionContext.Users.FirstOrDefaultAsync(x => x.EmailAddress == email);
            if (user == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordsalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordhash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await preAceleracionContext.Users.AddAsync(user);
            await preAceleracionContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
