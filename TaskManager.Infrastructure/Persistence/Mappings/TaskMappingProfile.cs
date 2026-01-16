using AutoMapper;
using TaskManager.Domain.Entities;
using Task = TaskManager.Infrastructure.Persistence.DbEntities.Task;

namespace TaskManager.Infrastructure.Persistence.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Task, TaskItem>().ReverseMap();
        }
    }
}
