using AutoMapper;
using MediatR;
using StudentManagement.Application.StudentManagement.DTOs;
using StudentManagement.Domain.Interfaces.Respositories;

namespace StudentManagement.Application.StudentManagement.Queries.GetStudents
{
    public record GetStudentQueryRequest(int id) : IRequest<GetStudentQueryResponse>;

    public record GetStudentQueryResponse(StudentDTO student);

    public class GetStudentQueryHandler : IRequestHandler<GetStudentQueryRequest, GetStudentQueryResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
        public async Task<GetStudentQueryResponse> Handle(GetStudentQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.GetByIdAsync(request.id);

            if (result == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            var dto = _mapper.Map<StudentDTO>(result);
            var response = new GetStudentQueryResponse(dto);

            return response;
        }
    }
}