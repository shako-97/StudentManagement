

namespace StudentManagement.Domain.Entities
{
	public class Student
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public DateTime CreatedAt { get; set; }

        public Student()
        {
            
        }
        public Student(string firstName, string lastName, string email, int age)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Age = age;
			CreatedAt = DateTime.Now;
		}
		public void Update(string firstName, string lastName, string email, int age)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Age = age;
		}
	}
}
