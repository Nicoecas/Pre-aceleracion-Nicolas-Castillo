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
            
            List<Character> internalvariable = await preAceleracionContext.Characters.ToListAsync();
            List<CharacterMovie> movieList = await preAceleracionContext.characterMovies.ToListAsync();
            Character a = (from character in internalvariable
                           join movies in movieList on
                           character.IdCharacter equals movies.CharacterId
                           select character).FirstOrDefault(x => x.Name == name);
            if (a == null)
            {
                a=internalvariable.FirstOrDefault(x => x.Name == name);
            }
            return a;
        }
        public async Task<IEnumerable<Character>> GetAge(int age)
        {
            List<Character> internalvariable = await preAceleracionContext.Characters.ToListAsync();
            List<CharacterMovie> movieList = await preAceleracionContext.characterMovies.ToListAsync();
            List<Character> a = (from character in internalvariable
                                 join movies in movieList on
                                 character.IdCharacter equals movies.CharacterId
                                 where character.Age == age
                                 select character).Distinct().ToList();
            List<Character> b= preAceleracionContext.Characters.Where(X=>X.Age==age).ToList();
            List<Character> c = a.Concat(b).Distinct().ToList();
            return c;
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
            await preAceleracionContext.Characters.AddAsync(characterToCreate);
            await preAceleracionContext.SaveChangesAsync();
            var mayor = preAceleracionContext.Characters.Max(x => x.IdCharacter);
            foreach (int movie in characterDto.MoviesId)
            {
                if (preAceleracionContext.characterMovies.FirstOrDefault(x => x.CharacterId == mayor && x.MovieId == movie) == null)
                {
                    var characterMovie = new CharacterMovie();
                    characterMovie.CharacterId = mayor;
                    characterMovie.MovieId = movie;
                    characterToCreate.Movies.Add(characterMovie);
                    preAceleracionContext.characterMovies.Add(characterMovie);
                    preAceleracionContext.SaveChanges();
                }
            }
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
