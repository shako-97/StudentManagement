using AutoMapper;
using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Shared;
using StudentManagement.Application.StudentManagement.DTOs;
using StudentManagement.Domain.Interfaces.Respositories;

namespace StudentManagement.Application.StudentManagement.Queries.GetStudents
{
    public class GetStudentsQueryRequest : PaginationRequest, IRequest<GetStudentsQueryResponse>
    {
        public GetStudentsQueryRequest(int page, int pageSize, string? search)
        {
            PageSize = pageSize;
            Page = page;
            Search = search;
        }

        public string? Search { get; set; }
    }
    public class GetStudentsQueryResponse : PaginationResponse
    {
        public IEnumerable<StudentDTO>? Students { get; set; }
    }

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQueryRequest, GetStudentsQueryResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public GetStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
        public async Task<GetStudentsQueryResponse> Handle(GetStudentsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _studentRepository.Query();
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(s => s.FirstName.Contains(request.Search) ||
                                         s.LastName.Contains(request.Search) ||
                                         s.Email.Contains(request.Search));
            }

            var total = await query.CountAsync();

            var results = await query.Skip(request.PageSize * (request.Page! - 1))
                                     .Take(request.PageSize!)
                                     .ToListAsync();


            var dtos = _mapper.Map<List<StudentDTO>>(results);
            var response = new GetStudentsQueryResponse
            {
                Students = dtos,
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total
            };

            return response;
        }
    }
}
