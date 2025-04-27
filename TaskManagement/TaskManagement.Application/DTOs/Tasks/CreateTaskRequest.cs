using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs.Tasks
{
    public class CreateTaskRequest
    {
        /// <summary>
        /// Task title
        /// </summary>
        [Required(ErrorMessage = "Task title is required!")]
        [StringLength(100, ErrorMessage = "Task title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Task assigned to user
        /// </summary>
        [Required(ErrorMessage = "Task Assignee is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Assigned user ID must be a positive integer.")]
        public int AssignedToUserId { get; set; }
    }
}
