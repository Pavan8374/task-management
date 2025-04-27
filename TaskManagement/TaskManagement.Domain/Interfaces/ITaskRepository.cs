namespace TaskManagement.Domain.Interfaces
{
    /// <summary>
    /// Task repository interface
    /// </summary>
    public interface ITaskRepository : IBaseRepository<Domain.Entities.Task>
    {
        /// <summary>
        /// Get rask by task identity
        /// </summary>
        /// <param name="id">Task identity</param>
        /// <returns>Task</returns>
        public Task<Domain.Entities.Task> GetTask(int id);

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns>Tasks list</returns>
        public Task<List<Domain.Entities.Task>> GetAllTasks();

        /// <summary>
        /// Get all tasks assigned to user
        /// </summary>
        /// <param name="userId">Assigned user identity</param>
        /// <returns>Task list</returns>
        public Task<List<Domain.Entities.Task>> GetAllTasksAssignedToUser(int userId);

    }
}
