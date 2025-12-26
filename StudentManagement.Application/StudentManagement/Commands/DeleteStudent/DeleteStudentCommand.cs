using MediatR;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Interfaces.Respositories;

namespace StudentManagement.Application.StudentManagement.Commands.DeleteStudent
{
    public record DeleteStudentCommand(int Id) : IRequest<Unit>;    

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student not found for Id:{request.Id}");
            }

            _studentRepository.Delete(student);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
