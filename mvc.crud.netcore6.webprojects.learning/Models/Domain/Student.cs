namespace mvc.crud.netcore6.webprojects.learning.Models.Domain
{
    public class Student
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? EnrollmentNumber { get; set; }

        public string? Email { get; set; }

        public string? MobileNo { get; set; }

        public string? Grade { get; set; }

        public string? Section { get; set; }

        public DateTime? Dob { get; set; }

        public string? HouseGroup { get; set; }

        public string? Gender { get; set; }

        public string? BloodGroup { get; set; }
    }
}
