namespace Mindworking.Models.CurriculumVitae;

[GraphQLName("Project")]
[GraphQLDescription("Represents a project undertaken by a candidate, potentially associated with a company.")]
public class Project
{
    [GraphQLDescription("The unique identifier for the project.")]
    public int Id { get; set; }
    [GraphQLDescription("The title of the project.")]
    public required string Title { get; set; }
    [GraphQLDescription("A detailed description of the project.")]
    public string? Description { get; set; }
    [GraphQLDescription("The start date of the project.")]
    public DateTime? From { get; set; }
    [GraphQLDescription("The end date of the project.")]
    public DateTime? To { get; set; }

    [GraphQLDescription("The identifier of the company associated with this project, if any.")]
    public int? CompanyId { get; set; }
    [GraphQLDescription("The company associated with this project, if any.")]
    public Company? Company { get; set; }

    [GraphQLDescription("The identifier of the candidate who undertook this project.")]
    public int CandidateId { get; set; }
    [GraphQLDescription("The candidate who undertook this project.")]
    public Candidate Candidate { get; set; } = null!;
    
    [GraphQLDescription("The list of project-skill associations for this project.")]
    public List<ProjectSkill> ProjectSkills { get; set; } = new();
}