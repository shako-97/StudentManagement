using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.StudentManagement.Commands.CreateStudent;
using StudentManagement.Application.StudentManagement.Commands.DeleteStudent;
using StudentManagement.Application.StudentManagement.Commands.UpdateStudent;
using StudentManagement.Application.StudentManagement.Queries.GetStudents;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand model)
        {
            var result = await _mediator.Send(model);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<ActionResult<GetStudentsQueryResponse>> Get(string? search, int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetStudentsQueryRequest(page, pageSize, search));

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentQueryResponse>> Get([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetStudentQueryRequest(id));

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStudentCommand dto)
        {
            dto.SetId(id);
            var result = await _mediator.Send(dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteStudentCommand(id));

            return Ok();
        }
    }
}
