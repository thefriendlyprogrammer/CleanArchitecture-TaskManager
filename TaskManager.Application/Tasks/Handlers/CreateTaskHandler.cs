using TaskManager.Application.Tasks.Commands;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using MediatR;

namespace TaskManager.Application.Tasks.Handlers
{
    public class CreateTaskHandler(ITaskRepository task) : IRequestHandler<CreateTaskCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                IsCompleted = false
            };

            await task.AddTask(taskEntity, cancellationToken);

            return taskEntity.Id;
        }
    }
}
