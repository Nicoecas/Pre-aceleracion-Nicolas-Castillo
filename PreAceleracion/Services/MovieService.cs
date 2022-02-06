using Microsoft.EntityFrameworkCore;
using PreAceleracion.Data;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreAceleracion.Services
{
    public class MovieService : GenericRepository<Movie>
    {
        private readonly PreAceleracionContext preAceleracionContext;
        public MovieService(PreAceleracionContext preAceleracionContext) : base(preAceleracionContext)
        {
            this.preAceleracionContext = preAceleracionContext;
        }

        //Get
        public async Task<List<MovieDtoGet>> GetAllMovie()
        {
            
            List<Movie> movieList = await preAceleracionContext.Movies.ToListAsync();
            List<MovieDtoGet> movieDtoList = new List<MovieDtoGet>();
            foreach(Movie movie in movieList){
                MovieDtoGet movieDto = new MovieDtoGet();
                movieDto.Title = movie.Title;
                movieDto.Image = movie.Image;
                movieDto.Date = movie.Date;
                movieDtoList.Add(movieDto);
            }
            return movieDtoList;
        }

        public async Task<Movie> GetName(string title)
        {
            
            List<Movie> internalvariable = await preAceleracionContext.Movies.ToListAsync();
            List<CharacterMovie> movieList = await preAceleracionContext.characterMovies.ToListAsync();
            Movie a = (from movie in internalvariable
                       join characters in movieList on
                       movie.IdMovie equals characters.MovieId
                       select movie).FirstOrDefault(x => x.Title == title);
            if (a == null)
            {
                a = internalvariable.FirstOrDefault(x => x.Title == title);
            }
            return a;
        }

        public async Task<IEnumerable<Movie>> GetGenre(int genre)
        {
            List<Movie> internalvariable = await preAceleracionContext.Movies.Where(x=>x.GenreId == genre).ToListAsync();
            List<CharacterMovie> movieList = await preAceleracionContext.characterMovies.ToListAsync();
            IEnumerable<Movie> a = (from movie in internalvariable
                                    join characters in movieList on
                                    movie.IdMovie equals characters.MovieId
                                    select movie).Distinct();
            List<Movie> b = preAceleracionContext.Movies.Where(X => X.GenreId == genre).ToList();
            List<Movie> c = a.Concat(b).Distinct().ToList();
            return c;
        }

        public List<Movie> Orden(string orden)
        {
            List<Movie> lista = preAceleracionContext.Movies.ToList();
            List<CharacterMovie> movieList = preAceleracionContext.characterMovies.ToList();

            if (orden == "ASC")
            {
                List<Movie> lista2 = (from d in lista
                                      join characters in movieList on
                                      d.IdMovie equals characters.MovieId
                                      orderby d.Title select d).ToList();
                lista2 = lista2.Concat(lista).Distinct().OrderBy(x=>x.Title).ToList();
                return lista2;
            }
            List<Movie> lista3 = (from d in lista
                   join characters in movieList on
                   d.IdMovie equals characters.MovieId
                   select d).ToList();
            lista3 = lista3.Concat(lista).Distinct().OrderByDescending(x => x.Title).ToList();
            return lista3;
        }

        //Post
        public async Task<bool> AddMovie(MovieDtoAdd movieDto)
        {
            var movieToCreate = new Movie();
            movieToCreate.Title = movieDto.Title;
            movieToCreate.Date = movieDto.Date;
            movieToCreate.Image = movieDto.Image;
            if (movieDto.Calification>5 || movieDto.Calification < 0)
            {
                return false;
            }
            movieToCreate.Calification = movieDto.Calification;
            movieToCreate.GenreId = movieDto.GenreId;
            preAceleracionContext.Movies.Attach(movieToCreate);
            Genre a = preAceleracionContext.Genres.FirstOrDefault(x => x.IdGenero == movieToCreate.GenreId);
            a.Movies.Add(movieToCreate);
            await preAceleracionContext.Movies.AddAsync(movieToCreate);
            await preAceleracionContext.SaveChangesAsync();
            var mayor = preAceleracionContext.Movies.Max(x => x.IdMovie);
            foreach (int character in movieDto.CharactersId)
            {

                if (preAceleracionContext.characterMovies.FirstOrDefault(x => x.MovieId == mayor && x.CharacterId == character) == null)
                {
                    var characterMovie = new CharacterMovie();
                    characterMovie.MovieId = mayor;
                    characterMovie.CharacterId = character;
                    movieToCreate.Characters.Add(characterMovie);
                    preAceleracionContext.characterMovies.Add(characterMovie);
                    preAceleracionContext.SaveChanges();
                }
            }
            return true;
        }

        //Put
        public async Task<bool> PutMovie(MovieDtoPut movieDto)
        {
            var internalvariable = await preAceleracionContext.Movies.FirstOrDefaultAsync(x => x.IdMovie == movieDto.IdMovie);
            internalvariable.Title = movieDto.Title;
            internalvariable.Image = movieDto.Image;
            internalvariable.Date = movieDto.Date;
            if (movieDto.Calification > 5 || movieDto.Calification < 0)
            {
                return false;
            }
            internalvariable.Calification = movieDto.Calification;
            preAceleracionContext.SaveChanges();
            return true;
        }
    }
}
