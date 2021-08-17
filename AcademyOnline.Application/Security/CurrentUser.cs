using AcademyOnline.Application.Contracts;
using AcademyOnline.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Security
{
    public class CurrentUser
    {
        public class CurrentUserQuery : IRequest<UserDto>
        {

        }

        public class CurrentUserHandler : IRequestHandler<CurrentUserQuery, UserDto>
        {
            private readonly UserManager<User> userManager;
            private readonly IJwtGenerator jwtGenerator;
            private readonly IUserSession userSession;

            public CurrentUserHandler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSession userSession)
            {
                this.userManager = userManager;
                this.jwtGenerator = jwtGenerator;
                this.userSession = userSession;
            }

            public async Task<UserDto> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(userSession.GetUserSession());
                return new UserDto
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Token = jwtGenerator.GenerateToken(user),
                    Image = null,
                    Email = user.Email
                }; 
            }
        }
    }
}
