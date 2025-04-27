using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Task controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Task controller constructor
        /// </summary>
        /// <param name="taskService">Task service</param>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Create task and assign to user
        /// </summary>
        /// <param name="createTaskRequest">Create task request model</param>
        /// <returns>Task Dto</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUpAsync([FromBody] CreateTaskRequest createTaskRequest)
        {
            var response = await _taskService.AddTask(createTaskRequest);
            return Ok(ApiResponse<TaskDto>.SuccessResponse(response));
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="Id">Task identity</param>
        /// <returns>Task dto</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTask(int id)
        {
            var response = await _taskService.GetTask(id);
            return Ok(ApiResponse<TaskDto>.SuccessResponse(response));
        }

        /// <summary>
        /// Get all tasks assigned to user
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>Task dto list</returns>
        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<TaskDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTasksAssignedToUser(int userId)
        {
            var response = await _taskService.GetAllTasksAssignedToUser(userId);
            return Ok(ApiResponse<List<TaskDto>>.SuccessResponse(response));
        }
    }
}
