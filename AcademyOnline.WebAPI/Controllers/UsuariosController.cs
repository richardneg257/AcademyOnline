using AcademyOnline.Application.Security;
using AcademyOnline.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcademyOnline.WebAPI.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsuariosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] Login.LoginQuery login)
        {
            return await mediator.Send(login);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] Register.RegisterQuery data)
        {
            return await mediator.Send(data);
        }
    }
}
