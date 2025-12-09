namespace Mindworking.GraphQl.CurriculumVitae;

[GraphQLDescription("Input type for adding a new candidate.")]
public record AddCandidateInput(string Name, string? Description, DateTime? DateOfBirth, string? Email);
[GraphQLDescription("Input type for adding a new company.")]
public record AddCompanyInput(int CandidateId, string Name, string? Website, string? Description, DateTime? Started, DateTime? Ended);
[GraphQLDescription("Input type for adding a new project.")]
public record AddProjectInput(int CandidateId, int? CompanyId, string Title, string? Description, DateTime? From, DateTime? To);
[GraphQLDescription("Input type for adding a new skill.")]
public record AddSkillInput(int CandidateId, string Name, string? Level);
[GraphQLDescription("Input type for adding a new education record.")]
public record AddEducationInput(int CandidateId, string Institution, string Degree, DateTime? From, DateTime? To, string? Description);

[GraphQLDescription("Input type for updating an existing candidate.")]
public record UpdateCandidateInput(int Id, string? Name, string? Description, DateTime? DateOfBirth, string? Email);
[GraphQLDescription("Input type for updating an existing company.")]
public record UpdateCompanyInput(int Id, string? Name, string? Website, string? Description, DateTime? Started, DateTime? Ended, int? CandidateId);
[GraphQLDescription("Input type for updating an existing project.")]
public record UpdateProjectInput(int Id, string? Title, string? Description, DateTime? From, DateTime? To, int? CompanyId, int? CandidateId);
[GraphQLDescription("Input type for updating an existing skill.")]
public record UpdateSkillInput(int Id, string? Name, string? Level, int? CandidateId);
[GraphQLDescription("Input type for updating an existing education record.")]
public record UpdateEducationInput(int Id, string? Institution, string? Degree, DateTime? From, DateTime? To, string? Description, int? CandidateId);