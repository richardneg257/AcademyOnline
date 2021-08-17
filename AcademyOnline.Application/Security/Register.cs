using AcademyOnline.Application.Contracts;
using AcademyOnline.Application.Handlers;
using AcademyOnline.Domain;
using AcademyOnline.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Security
{
    public class Register
    {
        public class RegisterQuery : IRequest<UserDto>
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class RegisterQueryValidato : AbstractValidator<RegisterQuery>
        {
            public RegisterQueryValidato()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class RegisterHandler : IRequestHandler<RegisterQuery, UserDto>
        {
            private readonly AcademyOnlineContext context;
            private readonly UserManager<User> userManager;
            private readonly IJwtGenerator jwtGenerator;

            public RegisterHandler(AcademyOnlineContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
            {
                this.context = context;
                this.userManager = userManager;
                this.jwtGenerator = jwtGenerator;
            }

            public async Task<UserDto> Handle(RegisterQuery request, CancellationToken cancellationToken)
            {
                var exist = await context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (exist)
                    throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con este email" });

                var existUserName = await context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if(existUserName)
                    throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con este UserName" });

                var user = new User()
                {
                    FullName = $"{request.Name} {request.LastName}",
                    Email = request.Email,
                    UserName = request.UserName
                };

                var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                    return new UserDto()
                    {
                        FullName = user.FullName,
                        Token = jwtGenerator.GenerateToken(user),
                        UserName = user.UserName,
                        Email = user.Email
                    };

                throw new Exception("No se pudo agregar al nuevo usuario");
            }
        }
    }
}
