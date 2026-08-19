namespace Backend.Models;
public class Assignment
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid AssignmentTypeId { get; set; }
    public string Label { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
    public Course Course { get; set; } = null!;
    public AssignmentType AssignmentType { get; set; } = null!;
    public ICollection<Grade> Grades { get; set; } = [];
}