namespace Mindworking.Models.CurriculumVitae;

[GraphQLName("Skill")]
[GraphQLDescription("Represents a skill possessed by a candidate.")]
public class Skill
{
    [GraphQLDescription("The unique identifier for the skill.")]
    public int Id { get; set; }
    [GraphQLDescription("The name of the skill.")]
    public required string Name { get; set; }
    [GraphQLDescription("The proficiency level of the skill (e.g., Beginner, Intermediate, Expert).")]
    public string? Level { get; set; }

    [GraphQLDescription("The identifier of the candidate who possesses this skill.")]
    public int CandidateId { get; set; }
    [GraphQLDescription("The candidate who possesses this skill.")]
    public Candidate Candidate { get; set; } = null!;
    
    [GraphQLDescription("The list of project-skill associations for this skill.")]
    public List<ProjectSkill> ProjectSkills { get; set; } = new();
}