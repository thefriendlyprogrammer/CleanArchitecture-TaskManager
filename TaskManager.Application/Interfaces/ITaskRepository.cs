using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasks(CancellationToken cancellationToken);
        Task<TaskItem> GetTaskById(Guid id, CancellationToken cancellationToken);
        Task AddTask(TaskItem task, CancellationToken cancellationToken);
        Task UpdateTask(TaskItem task, CancellationToken cancellationToken);
        Task DeleteTaskById(Guid id, CancellationToken cancellationToken);
    }
}
