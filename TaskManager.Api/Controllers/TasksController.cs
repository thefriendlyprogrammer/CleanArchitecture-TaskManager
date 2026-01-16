using TaskManager.Application.Tasks.Commands;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Common;
using MediatR;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(ApiResponse<Guid>.Ok(result));
        }
    }
}
