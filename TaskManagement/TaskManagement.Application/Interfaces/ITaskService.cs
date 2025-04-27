using TaskManagement.Application.DTOs.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService : IBaseService<Domain.Entities.Task>
    {
        /// <summary>
        /// Add new task
        /// </summary>
        /// <param name="createTaskRequest">Create task request</param>
        /// <returns>Add new task</returns>
        public Task<TaskDto> AddTask(CreateTaskRequest createTaskRequest);

        /// <summary>
        /// Get rask by task identity
        /// </summary>
        /// <param name="id">Task identity</param>
        /// <returns>Task dto</returns>
        public Task<TaskDto> GetTask(int id);

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns>Task dto list</returns>
        public Task<List<TaskDto>> GetAllTasks();

        /// <summary>
        /// Get all tasks assigned to user
        /// </summary>
        /// <param name="userId">Assigned user identity</param>
        /// <returns>Task dto list</returns>
        public Task<List<TaskDto>> GetAllTasksAssignedToUser(int userId);
    }
}
