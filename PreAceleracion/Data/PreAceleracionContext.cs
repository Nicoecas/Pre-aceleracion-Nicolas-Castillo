using Microsoft.EntityFrameworkCore;
using PreAceleracion.Entities;

namespace PreAceleracion.Data
{
    public class PreAceleracionContext:DbContext
    {
        public PreAceleracionContext(DbContextOptions<PreAceleracionContext> options) : base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CharacterMovie> characterMovies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
