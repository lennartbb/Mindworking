namespace Mindworking.Models.CurriculumVitae;

[GraphQLName("Education")]
[GraphQLDescription("Represents an educational qualification or experience of a candidate.")]
public class Education
{
    [GraphQLDescription("The unique identifier of the education record.")]
    public int Id { get; set; }
    [GraphQLDescription("The institution where the education was obtained.")]
    public required string Institution { get; set; }
    [GraphQLDescription("The degree or qualification obtained.")]
    public required string Degree { get; set; }
    [GraphQLDescription("Education start date.")]
    public DateTime? From { get; set; }
    [GraphQLDescription("Education end date.")]
    public DateTime? To { get; set; }
    [GraphQLDescription("Additional details about the education.")]
    public string? Description { get; set; }
    
    [GraphQLDescription("The identifier of the candidate associated with this education record.")]
    public int CandidateId { get; set; }
    [GraphQLDescription("The candidate associated with this education record.")]
    public Candidate Candidate { get; set; } = null!;

}