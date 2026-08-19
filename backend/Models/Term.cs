using Backend.Enums;

namespace Backend.Models;

public class Term
{
    public TermSeason TermSeason { get; set; }
    public int TermYear { get; set; }
    public ICollection<Course> Courses { get; set; } = [];
}