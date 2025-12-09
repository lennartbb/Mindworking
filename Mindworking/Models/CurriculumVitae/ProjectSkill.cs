namespace Mindworking.Models.CurriculumVitae;

[GraphQLName("ProjectSkill")]
[GraphQLDescription("Represents the association between a project and a skill used in that project.")]
public class ProjectSkill
{
    [GraphQLDescription("The unique identifier for the ProjectSkill entry.")]
    public int ProjectId { get; set; }
    [GraphQLDescription("The project associated with this skill.")]
    public Project Project { get; set; } = null!;

    [GraphQLDescription("The unique identifier for the skill associated with the project.")]
    public int SkillId { get; set; }
    [GraphQLDescription("The skill associated with this project.")]
    public Skill Skill { get; set; } = null!;
}