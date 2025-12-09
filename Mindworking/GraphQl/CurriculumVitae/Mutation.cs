using HotChocolate;
using Mindworking.Data.CurriculumVitae;
using Mindworking.Models.CurriculumVitae;

namespace Mindworking.GraphQl.CurriculumVitae;

public class Mutation
{
    [GraphQLDescription("Adds a new candidate to the database.")]
    public async Task<Candidate> AddCandidateAsync(AddCandidateInput input,
        [Service] CurriculumVitaeDbContext dbContext)
    {
        var candidate = new Candidate
        {
            Name = input.Name,
            Description = input.Description,
            DateOfBirth = input.DateOfBirth,
            Email = input.Email
            
        };
            dbContext.Candidates.Add(candidate);
            await dbContext.SaveChangesAsync();
            return candidate;
    }

    [GraphQLDescription("Updates an existing candidate's information.")]
    public async Task<Candidate?> UpdateCandidateAsync(UpdateCandidateInput input,
        [Service] CurriculumVitaeDbContext dbContext)
    {
        var candidate = await dbContext.Candidates.FindAsync(input.Id);
        if (candidate == null) return null;

        candidate.Name = input.Name ?? candidate.Name;
        candidate.Description = input.Description ?? candidate.Description;
        candidate.DateOfBirth = input.DateOfBirth ?? candidate.DateOfBirth;
        candidate.Email = input.Email ?? candidate.Email;

        await dbContext.SaveChangesAsync();
        return candidate;
    }
    
    [GraphQLDescription("Deletes a candidate from the database.")]
    public async Task<bool> DeleteCandidateAsync(int candidateId, [Service] CurriculumVitaeDbContext dbContext)
    {
        var candidate = await dbContext.Candidates.FindAsync(candidateId);
        if (candidate == null) 
            return false;
        dbContext.Candidates.Remove(candidate);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    [GraphQLDescription("Adds a new company to the database.")]
    public async Task<Company?> AddCompanyAsync(AddCompanyInput input, [Service] CurriculumVitaeDbContext dbContext)
    {
        var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
        if (candidate == null) return null;

        var company = new Company
        {
            CandidateId = input.CandidateId,
            Name = input.Name,
            Website = input.Website,
            Description = input.Description,
            Started = input.Started,
            Ended = input.Ended
        };
        dbContext.Companies.Add(company);
        await dbContext.SaveChangesAsync();
        return company;
    }
    
    [GraphQLDescription("Updates an existing company's information.")]
    public async Task<Company?> UpdateCompanyAsync(UpdateCompanyInput input,
        [Service] CurriculumVitaeDbContext dbContext)
    {
        var company = await dbContext.Companies.FindAsync(input.Id);
        if (company == null) return null;

        if (input.CandidateId != null)
        {
            var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
            if (candidate == null) return null;
        }
        
        company.Name = input.Name ?? company.Name;
        company.Website = input.Website ?? company.Website;
        company.Description = input.Description ?? company.Description;
        company.Started = input.Started ?? company.Started;
        company.Ended = input.Ended ?? company.Ended;

        await dbContext.SaveChangesAsync();
        return company;
    }

    [GraphQLDescription("Deletes a company from the database.")]
    public async Task<bool> DeleteCompanyAsync(int companyId, [Service] CurriculumVitaeDbContext dbContext)
    {
        var company = await dbContext.Companies.FindAsync(companyId);
        if (company == null) return false;

        dbContext.Companies.Remove(company);
        await dbContext.SaveChangesAsync();
        return true;
    }

    [GraphQLDescription("Adds a new project to the database.")]
    public async Task<Project?> AddProjectAsync(AddProjectInput input, [Service] CurriculumVitaeDbContext dbContext)
    {
        var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
        if (candidate == null) return null;
        
        var project = new Project
        {
            CandidateId = input.CandidateId,
            CompanyId = input.CompanyId,
            Title = input.Title,
            Description = input.Description,
            From = input.From,
            To = input.To
        };
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();
        return project;
    }
    
    [GraphQLDescription("Updates an existing project's information.")]
    public async Task<Project?> UpdateProjectAsync(UpdateProjectInput input,
        [Service] CurriculumVitaeDbContext dbContext)
    {
        var project = await dbContext.Projects.FindAsync(input.Id);
        if (project == null) return null;

        if (input.CandidateId != null)
        {
            var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
            if (candidate == null) return null;
        }
        
        project.Title = input.Title ?? project.Title;
        project.Description = input.Description ?? project.Description;
        project.From = input.From ?? project.From;
        project.To = input.To ?? project.To;
        project.CompanyId = input.CompanyId ?? project.CompanyId;

        await dbContext.SaveChangesAsync();
        return project;
    }
    
    [GraphQLDescription("Deletes a project from the database.")]
    public async Task<bool> DeleteProjectAsync(int projectId, [Service] CurriculumVitaeDbContext dbContext)
    {
        var project = await dbContext.Projects.FindAsync(projectId);
        if (project == null) return false;

        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
        return true;
    }

    [GraphQLDescription("Adds a new skill to the database.")]
    public async Task<Skill?> AddSkillAsync(AddSkillInput input, [Service] CurriculumVitaeDbContext dbContext)
    {   
        var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
        if (candidate == null) return null;
            
        var skill = new Skill
        {
            CandidateId = input.CandidateId,
            Name = input.Name,
            Level = input.Level
        };
        dbContext.Skills.Add(skill);
        await dbContext.SaveChangesAsync();
        return skill;
    }
    
    [GraphQLDescription("Updates an existing skill's information.")]
    public async Task<Skill?> UpdateSkillAsync(UpdateSkillInput input,
        [Service] CurriculumVitaeDbContext dbContext)
    {
        if (input.CandidateId != null)
        {
            var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
            if (candidate == null) return null;
        }
        
        var skill = await dbContext.Skills.FindAsync(input.Id);
        if (skill == null) return null;

        skill.Name = input.Name ?? skill.Name;
        skill.Level = input.Level ?? skill.Level;

        await dbContext.SaveChangesAsync();
        return skill;
    }
    
    [GraphQLDescription("Deletes a skill from the database.")]
    public async Task<bool> DeleteSkillAsync(int skillId, [Service] CurriculumVitaeDbContext dbContext)
    {
        var skill = await dbContext.Skills.FindAsync(skillId);
        if (skill == null) return false;

        dbContext.Skills.Remove(skill);
        await dbContext.SaveChangesAsync();
        return true;
    }

    [GraphQLDescription("Adds a new education entry to the database.")]
    public async Task<Education?> AddEducationAsync(AddEducationInput input,
        [Service] CurriculumVitaeDbContext dbContext)
    {
        var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
        if (candidate == null) return null;
        
        var education = new Education
        {
            CandidateId = input.CandidateId,
            Institution = input.Institution,
            Degree = input.Degree,
            From = input.From,
            To = input.To,
            Description = input.Description
            
        };
        dbContext.Educations.Add(education);
        await dbContext.SaveChangesAsync();
        return education;
    }
    
    [GraphQLDescription("Updates an existing education entry's information.")]
    public async Task<Education?> UpdateEducationAsync(UpdateEducationInput input,
        [Service] CurriculumVitaeDbContext dbContext)
    {
        var education = await dbContext.Educations.FindAsync(input.Id);
        if (education == null) return null;
        
        if (input.CandidateId != null)
        {
            var candidate = await dbContext.Candidates.FindAsync(input.CandidateId);
            if (candidate == null) return null;
        }
        
        education.Institution = input.Institution ?? education.Institution;
        education.Degree = input.Degree ?? education.Degree;
        education.From = input.From ?? education.From;
        education.To = input.To ?? education.To;
        education.Description = input.Description ?? education.Description;
        education.CandidateId = input.CandidateId ?? education.CandidateId;

        await dbContext.SaveChangesAsync();
        return education;
    }
    
    [GraphQLDescription("Deletes an education entry from the database.")]
    public async Task<bool> DeleteEducationAsync(int educationId, [Service] CurriculumVitaeDbContext dbContext)
    {
        var education = await dbContext.Educations.FindAsync(educationId);
        if (education == null) return false;

        dbContext.Educations.Remove(education);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    [GraphQLDescription("Assigns a skill to a project.")]
    public async Task<bool> AssignSkillToProjectAsync(int projectId, int skillId, [Service] CurriculumVitaeDbContext dbContext)
    {
        var project = await dbContext.Projects.FindAsync(projectId);
        var skill = await dbContext.Skills.FindAsync(skillId);
        if (project == null || skill == null) return false;

        var projectSkill = new ProjectSkill
        {
            ProjectId = projectId,
            SkillId = skillId
        };
        dbContext.ProjectSkills.Add(projectSkill);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    [GraphQLDescription("Removes a skill from a project.")]
    public async Task<bool> RemoveSkillFromProjectAsync(int projectId, int skillId, [Service] CurriculumVitaeDbContext dbContext)
    {
        var projectSkill = await dbContext.ProjectSkills.FindAsync(projectId, skillId);
        if (projectSkill == null) return false;

        dbContext.ProjectSkills.Remove(projectSkill);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
