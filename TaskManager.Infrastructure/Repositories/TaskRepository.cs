using TaskManager.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;
using AutoMapper;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository(TaskManagerDbContext context, IMapper mapper) : ITaskRepository
    {
        public async Task AddTask(TaskItem task, CancellationToken cancellationToken)
        {
            var map = mapper.Map<Persistence.DbEntities.Task>(task);

            await context.Tasks.AddAsync(map, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteTaskById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskItem>> GetAllTasks(CancellationToken cancellationToken)
        {
            var dbTasks = await context.Tasks.AsNoTracking().ToListAsync(cancellationToken);

            return mapper.Map<List<TaskItem>>(dbTasks);
        }

        public Task<TaskItem> GetTaskById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTask(TaskItem task, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
