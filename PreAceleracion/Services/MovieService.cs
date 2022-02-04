using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PreAceleracion.Data;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreAceleracion.Services
{
    public class MovieService : GenericRepository<Movie>
    {
        private readonly PreAceleracionContext preAceleracionContext;
        private readonly IMapper mapper;
        public MovieService(PreAceleracionContext preAceleracionContext, IMapper mapper) : base(preAceleracionContext)
        {
            this.preAceleracionContext = preAceleracionContext;
            this.mapper = mapper;
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
            return await preAceleracionContext.Movies.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<List<Movie>> GetGenre(int genre)
        {
            return await preAceleracionContext.Movies.Where(x => x.GenreId == genre).ToListAsync();
        }

        public List<Movie> Orden(string orden)
        {
            List<Movie> lista = preAceleracionContext.Movies.ToList();

            if (orden == "ASC")
            {
                List<Movie> lista2 = (from d in lista orderby d.Title select d).ToList();
                return lista2;
            }
            return preAceleracionContext.Movies.OrderByDescending(x => x.Title).ToList();
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
            movieToCreate.Characters = movieDto.Characters;
            movieToCreate.GenreId = movieDto.GenreId;
            await preAceleracionContext.Movies.AddAsync(movieToCreate);
            await preAceleracionContext.SaveChangesAsync();
            preAceleracionContext.Movies.Attach(movieToCreate);
            Genre a = preAceleracionContext.Genres.FirstOrDefault(x => x.IdGenero == movieToCreate.GenreId);
            a.Movies.Add(movieToCreate);
            await preAceleracionContext.SaveChangesAsync();
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
