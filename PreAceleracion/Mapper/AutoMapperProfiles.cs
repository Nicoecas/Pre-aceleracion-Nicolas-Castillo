using AutoMapper;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;

namespace PreAceleracion.Mapper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            //Token
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>();

            //Get
            CreateMap<CharacterDtoGet, Character>();
            CreateMap<MovieDtoGet, Movie>();

            //Add
            CreateMap<CharacterDtoAdd, Character>();
            CreateMap<MovieDtoAdd, Movie>();
            CreateMap<GenreDtoAdd, Genre>();

            //Put
            CreateMap<CharacterDtoPut, Character>();
            CreateMap<MovieDtoPut, Movie>();
            CreateMap<GenreDtoPut, Genre>();
        }

    }
}
