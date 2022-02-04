using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PreAceleracion.Data;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreAceleracion.Services
{
    public class CharacterService : GenericRepository<Character>
    {
        private readonly PreAceleracionContext preAceleracionContext;
        public CharacterService(PreAceleracionContext preAceleracionContext) : base(preAceleracionContext)
        {
            this.preAceleracionContext = preAceleracionContext;
        }


        public async Task<List<CharacterDtoGet>> GetAllCharacter()
        {
            List<Character> characterList = await preAceleracionContext.Characters.ToListAsync();
            List<CharacterDtoGet> characterDtoList = new List<CharacterDtoGet>();
            foreach (Character character in characterList)
            {
                CharacterDtoGet characterDto = new CharacterDtoGet();
                characterDto.Name= character.Name;
                characterDto.Image= character.Image;
                characterDtoList.Add(characterDto);
                
            }
            return characterDtoList;
        }

        public async Task<Character> GetName(string name)
        {
            return await preAceleracionContext.Characters.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<List<Character>> GetAge(int age)
        {
            return await preAceleracionContext.Characters.Where(x => x.Age == age).ToListAsync();
        }
        public async Task<IEnumerable<Character>> GetForMovie(int movie)
        {
            List<Character> internalvariable = await preAceleracionContext.Characters.ToListAsync();
            List<CharacterMovie> movieList = await preAceleracionContext.characterMovies.ToListAsync();
            IEnumerable<Character> a = (from character in internalvariable
                     join movies in movieList on
                     character.IdCharacter equals movies.CharacterId
                     where movies.MovieId == movie
                     select character);
            return a;
        }
        

        //Post
        public async Task<CharacterDtoAdd> AddCharacter(CharacterDtoAdd characterDto)
        {
            var characterToCreate = new Character();
            characterToCreate.Name = characterDto.Name;
            characterToCreate.Age = characterDto.Age;
            characterToCreate.Weight = characterDto.Weight;
            characterToCreate.History = characterDto.History;
            characterToCreate.Movies = characterDto.Movies;
            await preAceleracionContext.Characters.AddAsync(characterToCreate);
            await preAceleracionContext.SaveChangesAsync();
            return characterDto;
        }

        //Put
        public async Task<CharacterDtoPut> PutCharacter(CharacterDtoPut characterDto)
        {
            var internalvariable = await preAceleracionContext.Characters.FirstOrDefaultAsync(x => x.IdCharacter == characterDto.IdCharacter);
            internalvariable.Name = characterDto.Name;
            internalvariable.Image = characterDto.Image;
            internalvariable.Age = characterDto.Age;
            internalvariable.Weight = characterDto.Weight;
            internalvariable.History = characterDto.History;
            preAceleracionContext.SaveChanges();
            return characterDto;
        }

    }
}
