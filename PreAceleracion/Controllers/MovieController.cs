using Microsoft.AspNetCore.Mvc;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using PreAceleracion.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreAceleracion.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService movieService;
        public MovieController(MovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string name=null, string orden=null, string genre=null)
        {
            if (name != null)
            {
                return Ok(await movieService.GetName(name));
            }
            if(orden != null)
            {
                orden = orden.ToUpper();
                if ((orden != "ASC") && (orden != "DESC"))
                {
                    return BadRequest("Agrege los comandos ASC o DESC, en mayuscula");
                }
                return Ok(movieService.Orden(orden));
            }
            if (genre != null)
            {
                int valor = Int32.Parse(genre);
                return Ok(await movieService.GetGenre(valor));
            }
            return Ok(await movieService.GetAllMovie());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var entity = await movieService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> Delete(int id)
        {
            var entity = await movieService.Delete(id);
            if (entity == null)
            {
                return BadRequest("User Not Found");
            }
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult> Post(MovieDtoAdd movieDto)
        {
            if(await movieService.AddMovie(movieDto))
            {
                return Ok();
            }
            return BadRequest("Formato no válido");
        }

        [HttpPut]
        public async Task<ActionResult> Put(MovieDtoPut movieDto)
        {
            var moviePut = new Movie();
            moviePut.IdMovie = movieDto.IdMovie;
            var entity = await movieService.GetById(moviePut.IdMovie);
            if (entity == null)
            {
                return NotFound();
            }
            if (await movieService.PutMovie(movieDto))
            { 
            return Ok();
            }
            return BadRequest("Formato no válido");
        }
    }
}
