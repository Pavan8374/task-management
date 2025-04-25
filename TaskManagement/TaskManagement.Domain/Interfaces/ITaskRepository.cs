namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskRepository : IBaseRepository<Domain.Entities.Task>
    {
        public Task<Domain.Entities.Task> GetTask(int id);
        public Task<List<Domain.Entities.Task>> GetAllTasks();

    }
}
