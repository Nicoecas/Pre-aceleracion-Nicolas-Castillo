using Microsoft.EntityFrameworkCore;
using PreAceleracion.Data;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using System.Threading.Tasks;

namespace PreAceleracion.Services
{
    public class GenreService : GenericRepository<Genre>
    {
        private readonly PreAceleracionContext preAceleracionContext;
        public GenreService(PreAceleracionContext preAceleracionContext):base(preAceleracionContext)
        {
            this.preAceleracionContext=preAceleracionContext;
        }

        public async Task<GenreDtoAdd> AddGenre(GenreDtoAdd genreDto)
        {
            var genreToCreate = new Genre();
            genreToCreate.Name = genreDto.Name;
            genreToCreate.Image = genreDto.Image;
            await preAceleracionContext.Genres.AddAsync(genreToCreate);
            await preAceleracionContext.SaveChangesAsync();
            return genreDto;
        }
        public async Task<GenreDtoPut> PutGenre(GenreDtoPut genreDto)
        {
            var internalvariable = await preAceleracionContext.Genres.FirstOrDefaultAsync(x => x.IdGenero == genreDto.IdGenero);
            internalvariable.Name = genreDto.Name;
            internalvariable.Image = genreDto.Image;
            preAceleracionContext.SaveChanges();
            return genreDto;
        }


    }
}
