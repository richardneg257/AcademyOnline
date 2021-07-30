using AcademyOnline.Application.Security;
using AcademyOnline.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcademyOnline.WebAPI.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
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
    }
}
