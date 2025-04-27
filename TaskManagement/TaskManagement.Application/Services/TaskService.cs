using System.Data;
using TaskManagement.Application.Common;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Interfaces;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.Services
{
    public class TaskService : BaseService<Task>, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserContext _userContext;
        private readonly IUserService _userService;
        public TaskService(ITaskRepository taskRepository, IUserContext userContext, IUserService userService) : base(taskRepository)
        {
            _taskRepository = taskRepository;
            _userContext = userContext;
            _userService = userService;
        }

        /// <summary>
        /// Add new task
        /// </summary>
        /// <param name="createTaskRequest">Create a new task</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<TaskDto> AddTask (CreateTaskRequest createTaskRequest)
        {
            if(createTaskRequest == null)
                throw new BusinessException("Request body is empty.");

            bool isUserExist = await _userService.IsUserExist(createTaskRequest.AssignedToUserId);
            if (!isUserExist)
                throw new BusinessException("The assigned user not found!");

            var task = new Task
            {
                Title = createTaskRequest.Title,
                Description = createTaskRequest.Description,
                AssignedToUserId = createTaskRequest.AssignedToUserId,
                AssignedByUserId = _userContext.UserId
            };

            await _taskRepository.AddAsync(task);
            var newTask = await _taskRepository.GetTask(task.Id);
            return new TaskDto
            {
                Id = newTask.Id,
                Title = newTask.Title,
                Description = newTask.Description,
                TaskStatus = new TaskStatusDTO
                {
                    Id = (int)newTask.TaskStatus,
                    Status = newTask.TaskStatus.ToString()
                },
                AssignedByUser = new AssignedByUserDto
                {
                    UserId = newTask.AssignedByUserId,
                    FullName = newTask.AssignedByUser.FullName,
                    Email = newTask.AssignedByUser.Email
                },
                AssignedToUser = new AssignedToUserDto
                {
                    UserId = newTask.AssignedToUserId,
                    FullName = newTask.AssignedToUser.FullName,
                    Email = newTask.AssignedToUser.Email
                }
            };
        }

        /// <summary>
        /// Get all tasks 
        /// </summary>
        /// <returns>TaskDto list</returns>
        public async Task<List<TaskDto>> GetAllTasks()
        {
            var data = await _taskRepository.GetAllTasks();
            return data.Select(x => 
                new TaskDto
                {
                    Id = x.Id,
                    Title = x.Title, 
                    Description = x.Description,
                    TaskStatus = new TaskStatusDTO
                    {
                        Id = (int)x.TaskStatus,
                        Status = x.TaskStatus.ToString()
                    },
                    AssignedByUser = new AssignedByUserDto
                    {
                        UserId = x.AssignedByUserId,
                        FullName = x.AssignedByUser.FullName,
                        Email = x.AssignedByUser.Email 
                    },
                    AssignedToUser = new AssignedToUserDto
                    {
                        UserId = x.AssignedToUserId,
                        FullName = x.AssignedToUser.FullName,
                        Email = x.AssignedToUser.Email
                    } 
            }).ToList();
        }

        /// <summary>
        /// Get task by task identity
        /// </summary>
        /// <param name="id">Task identity</param>
        /// <returns>Task dto</returns>
        public async Task<TaskDto> GetTask(int id)
        {
            var data = await _taskRepository.GetTask(id);
            return new TaskDto
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                TaskStatus = new TaskStatusDTO
                {
                    Id = (int)data.TaskStatus,
                    Status = data.TaskStatus.ToString()
                },
                AssignedByUser = new AssignedByUserDto
                {
                    UserId = data.AssignedByUserId,
                    FullName = data.AssignedByUser.FullName,
                    Email = data.AssignedByUser.Email
                },
                AssignedToUser = new AssignedToUserDto
                {
                    UserId = data.AssignedToUserId,
                    FullName = data.AssignedToUser.FullName,
                    Email = data.AssignedToUser.Email
                }
            };
        }

        /// <summary>
        /// Get all tasks assigned to user
        /// </summary>
        /// <param name="userId">Assigned user identity</param>
        /// <returns>TaskDto list</returns>
        public async Task<List<TaskDto>> GetAllTasksAssignedToUser(int userId)
        {
            var data = await _taskRepository.GetAllTasksAssignedToUser(userId);
            return data.Select(x =>
                new TaskDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    TaskStatus = new TaskStatusDTO
                    {
                        Id = (int)x.TaskStatus,
                        Status = x.TaskStatus.ToString()
                    },
                    AssignedByUser = new AssignedByUserDto
                    {
                        UserId = x.AssignedByUserId,
                        FullName = x.AssignedByUser.FullName,
                        Email = x.AssignedByUser.Email
                    },
                    AssignedToUser = new AssignedToUserDto
                    {
                        UserId = x.AssignedToUserId,
                        FullName = x.AssignedToUser.FullName,
                        Email = x.AssignedToUser.Email
                    }
                }).ToList();
        }
     }
}
