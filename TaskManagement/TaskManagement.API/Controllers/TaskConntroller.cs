using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("/v{Version:ApiVersion}/[controller]")]
    public class TaskConntroller : ControllerBase
    {
        public TaskConntroller()
        {
            
        }
    }
}
