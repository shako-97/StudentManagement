using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Interfaces.Respositories;

namespace StudentManagement.Application.StudentManagement.Commands.CreateStudent
{
    public record CreateStudentCommand(string FirstName, string LastName, string Email, int Age) : IRequest<Unit>;

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Unit>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student(request.FirstName, request.LastName, request.Email, request.Age);

            await _studentRepository.AddStudentAsync(student);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
