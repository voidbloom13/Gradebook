namespace Backend.Models;

public class Student : User
{
    public ICollection<Enrollment> Enrollments { get; set; } = [];
    public ICollection<Grade> Grades { get; set; } = [];
}