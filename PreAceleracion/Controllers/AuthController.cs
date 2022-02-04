using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PreAceleracion.Data.Interfaces;
using PreAceleracion.Dtos;
using PreAceleracion.Entities;
using PreAceleracion.Services.Interfaces;
using System.Threading.Tasks;

namespace PreAceleracion.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;

        public AuthController(IAuthRepository _repo, ITokenService _tokenService, IMapper mapper)
        {
            this.mapper = mapper;
            this._repo = _repo;
            this._tokenService = _tokenService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto usuarioDto)
        {
            usuarioDto.EmailAddress = usuarioDto.EmailAddress.ToLower();
            if (await _repo.ExistUser(usuarioDto.EmailAddress))
            {
                return BadRequest("Correo ya registrado");
            }
            var usuarioNuevo = mapper.Map<User>(usuarioDto);
            var usuarioCreado = await _repo.Register(usuarioNuevo, usuarioDto.Password);
            return Ok(usuarioCreado);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var usuarioFromRepo = await _repo.Login(userLoginDto.EmailAdress, userLoginDto.Password);
            if (usuarioFromRepo == null)
            {
                return Unauthorized();
            }
            var token = _tokenService.CreateToken(usuarioFromRepo);
            return Ok(new
            {
                token = token,
                usuarioFromRepo = usuarioFromRepo
            });
        }
    }
}
