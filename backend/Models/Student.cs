namespace Backend.Models;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; } = [];
    public ICollection<Grade> Grades { get; set; } = [];
}