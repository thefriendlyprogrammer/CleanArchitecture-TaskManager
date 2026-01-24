using MediatR;
using TaskManager.Application.Features.Auth.Commands;

namespace TaskManager.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler(UserManager<>) : IRequestHandler<LoginCommand, string>
    {
        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
