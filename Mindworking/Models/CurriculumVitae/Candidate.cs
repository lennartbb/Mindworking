namespace Mindworking.Models.CurriculumVitae;

[GraphQLName("Candidate")]
[GraphQLDescription("Represents a candidate's curriculum vitae (CV) information.")]
public class Candidate
{
    [GraphQLDescription("The unique identifier for the candidate.")]
    public int Id { get; set; }
    [GraphQLDescription("The full name of the candidate.")]
    public required string Name { get; set; }
    [GraphQLDescription("A brief description or summary about the candidate.")]
    public string? Description { get; set; }
    [GraphQLDescription("The date of birth of the candidate.")]
    public DateTime? DateOfBirth { get; set; }
    [GraphQLDescription("The email address of the candidate.")]
    public string? Email { get; set; }

    [GraphQLDescription("List of companies where the candidate has worked.")]
    public List<Company> Companies { get; set; } = new();
    [GraphQLDescription("List of projects undertaken by the candidate.")]
    public List<Project> Projects { get; set; } = new();
    [GraphQLDescription("List of skills possessed by the candidate.")]
    public List<Skill> Skills { get; set; } = new();
    [GraphQLDescription("List of educational qualifications of the candidate.")]
    public List<Education> Educations { get; set; } = new();
    
    
    
}