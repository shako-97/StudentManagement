using AutoMapper;
using StudentManagement.Application.StudentManagement.DTOs;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Profiles
{
	public class StudentProfile : Profile
	{
		public StudentProfile()
		{
			CreateMap<Student, StudentDTO>();
		}
	}
}
