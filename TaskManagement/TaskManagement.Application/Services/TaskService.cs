using System.Data;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.Services
{
    public class TaskService : BaseService<Domain.Entities.Task>, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository) : base(taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<TaskDto> AddTask (CreateTaskRequest createTaskRequest)
        {
            var task = new Task
            {
                Title = createTaskRequest.Title,
                Description = createTaskRequest.Description,
                AssignedToUserId = createTaskRequest.AssignedToUserId,
            };
            await _taskRepository.AddAsync(task);
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TaskStatus = new TaskStatusDTO
                {
                    Id = (int)task.TaskStatus,
                    Status = task.TaskStatus.ToString()
                },
                AssignedByUser = new AssignedByUserDto
                {
                    UserId = task.AssignedByUserId,
                    FullName = task.AssignedByUser.FullName,
                    Email = task.AssignedByUser.Email
                },
                AssignedToUser = new AssignedToUserDto
                {
                    UserId = task.AssignedToUserId,
                    FullName = task.AssignedToUser.FullName,
                    Email = task.AssignedToUser.Email
                }
            };
        }
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
    }
}
