namespace CoursesInfoPortal.Models
{
    public class AddStudentInfoDto
    {
        public Guid StudentId { get; set; }
        public required string FullName { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public string? SelectedCourses { get; set; }
        public string? Message { get; set; }
        public DateTime? ContactedDate { get; set; }


    }
}
