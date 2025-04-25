using TaskManagement.Application.DTOs.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService : IBaseService<Domain.Entities.Task>
    {
        public Task<TaskDto> GetTask(int id);
        public Task<List<TaskDto>> GetAllTasks();
    }
}
