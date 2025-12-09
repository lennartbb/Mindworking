namespace Mindworking.Models.CurriculumVitae;

[GraphQLName("Company")]
[GraphQLDescription("Represents a company where a candidate is working / has worked.")]
public class Company
{
    [GraphQLDescription("The unique identifier for the company.")]
    public int Id { get; set; }
    [GraphQLDescription("The name of the company.")]
    public required string Name { get; set; }
    [GraphQLDescription("The website URL of the company.")]
    public string? Website { get; set; }
    [GraphQLDescription("A detailed description of the company.")]
    public string? Description { get; set; }
    [GraphQLDescription("The date when the candidate started working at the company.")]
    public DateTime? Started { get; set; }
    [GraphQLDescription("The date when the candidate ended working at the company.")]
    public DateTime? Ended { get; set; }
    
    [GraphQLDescription("The identifier of the candidate associated with this company.")]
    public int CandidateId { get; set; }
    [GraphQLDescription("The candidate associated with this company.")]
    public Candidate Candidate { get; set; } = null!;
    
    [GraphQLDescription("The list of projects associated with this company.")]
    public List<Project> Projects { get; set; } = new();
    

}