using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using PreAceleracion.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreAceleracion.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService genreService;
        public GenreController(GenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> Get()
        {
            return await genreService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetById(int id)
        {
            var entity = await genreService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> Delete(int id)
        {
            var entity = await genreService.Delete(id);
            if (entity == null)
            {
                return BadRequest("User Not Found");
            }
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult> Post(GenreDtoAdd genreDto)
        {
            await genreService.AddGenre(genreDto);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(GenreDtoPut genreDto)
        {
            var genrePut = new Genre();
            genrePut.IdGenero = genreDto.IdGenero;
            var entity = await genreService.GetById(genrePut.IdGenero);
            if (entity == null)
            {
                return NotFound();
            }
            await genreService.PutGenre(genreDto);
            return Ok();
        }
    }
}

