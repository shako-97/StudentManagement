using MediatR;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Interfaces.Respositories;

namespace StudentManagement.Application.StudentManagement.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<Unit>
    {
        public int Id { get; private set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student not found for Id:{request.Id}");
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.Age = request.Age;

            _studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
