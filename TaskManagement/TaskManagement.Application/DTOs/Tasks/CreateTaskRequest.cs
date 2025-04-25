using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs.Tasks
{
    public class CreateTaskRequest
    {
        [Required(ErrorMessage = "Task title is required!")]
        public string Title{ get; set; }
        public string Description{ get; set; }

        [Required(ErrorMessage = "Task Assignee is required!")]
        public int AssignedToUserId{ get; set; }
    }
}
