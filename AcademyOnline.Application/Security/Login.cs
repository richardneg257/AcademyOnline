using AcademyOnline.Application.Contracts;
using AcademyOnline.Application.Handlers;
using AcademyOnline.Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Security
{
    public class Login
    {
        public class LoginQuery : IRequest<UserDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginQueryValidation : AbstractValidator<LoginQuery>
        {
            public LoginQueryValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class LoginHandler : IRequestHandler<LoginQuery, UserDto>
        {
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;
            private readonly IJwtGenerator jwtGenerator;

            public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.jwtGenerator = jwtGenerator;
            }

            public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    throw new ExceptionHandler(HttpStatusCode.Unauthorized);

                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (result.Succeeded)
                    return new UserDto()
                    {
                        FullName = user.FullName,
                        Token = jwtGenerator.GenerateToken(user),
                        UserName = user.UserName,
                        Email = user.Email,
                        Image = null
                    };

                throw new ExceptionHandler(HttpStatusCode.Unauthorized);
            }
        }
    }
}
