namespace Backend.Models;
public class Term
{
    public string Season { get; set; } = null!;
    public int Year { get; set; }
    public ICollection<Course> Courses { get; set; } = [];
}