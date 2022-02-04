using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using PreAceleracion.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreAceleracion.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly CharacterService characterService;
        public CharacterController(CharacterService characterService)
        {
            this.characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string age=null, string name=null, string movies=null)
        {
            if (age != null)
            {
                int valor = Int32.Parse(age);
                return Ok(await characterService.GetAge(valor));
            }
            if (name != null)
            {
                List<Character> lista = new List<Character>();
                lista.Add(await characterService.GetName(name));
                if (lista[0] == null)
                {
                    return NotFound();
                }
                return Ok(lista);
            }
            if (movies != null)
            {
                int valor = Int32.Parse(movies);
                return Ok(await characterService.GetForMovie(valor));
            }
            return Ok(await characterService.GetAllCharacter());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetById(int id)
        {
            var entity = await characterService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> Delete(int id)
        {
            var entity = await characterService.Delete(id);
            if (entity == null)
            {
                return BadRequest("User Not Found");
            }
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CharacterDtoAdd characterDto)
        {
            await characterService.AddCharacter(characterDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put(CharacterDtoPut characterDto)
        {
            var characterPut = new Character();
            characterPut.IdCharacter = characterDto.IdCharacter;
            var entity = await characterService.GetById(characterPut.IdCharacter);
            if (entity == null)
            {
                return NotFound();
            }
            await characterService.PutCharacter(characterDto);
            return Ok();
        }
    }
}
