using Mindworking.Data.CurriculumVitae;
using Mindworking.Models.CurriculumVitae;

namespace Mindworking.GraphQl.CurriculumVitae;

public class Query
{
    [UseFirstOrDefault]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Gets companies")]
    [GraphQLName("GetCompanies")]
    public IQueryable<Company> GetCompanies([Service]CurriculumVitaeDbContext db) => db.Companies;
    
    [UseFirstOrDefault]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Gets projects")]
    [GraphQLName("GetProjects")]
    public IQueryable<Project> GetProjects([Service] CurriculumVitaeDbContext db) => db.Projects;
    
    [UseFirstOrDefault]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Gets skills")]
    [GraphQLName("GetSkills")]
    public IQueryable<Skill> GetSkills([Service] CurriculumVitaeDbContext db) => db.Skills;
    
    [UseFirstOrDefault]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Gets education")]
    [GraphQLName("GetEducations")]
    public IQueryable<Education> GetEducations([Service] CurriculumVitaeDbContext db) => db.Educations;

    [UseFirstOrDefault]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Gets candidates")]
    [GraphQLName("GetCandidates")]
    public IQueryable<Candidate> GetCandidates([Service] CurriculumVitaeDbContext db) => db.Candidates;}