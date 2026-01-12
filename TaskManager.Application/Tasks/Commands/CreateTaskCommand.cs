using MediatR;

namespace TaskManager.Application.Tasks.Commands
{
    public record CreateTaskCommand(string Title) : IRequest<Guid>;
}
