using MediatR;

namespace TaskManager.Application.Features.Tasks.Commands
{
    public record CreateTaskCommand(string Title) : IRequest<Guid>;
}
