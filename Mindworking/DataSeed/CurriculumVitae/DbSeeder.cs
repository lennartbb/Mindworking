using Mindworking.Models.CurriculumVitae;

namespace Mindworking.DataSeed.CurriculumVitae;

public static class DbSeeder
{
    public static void Seed(Mindworking.Data.CurriculumVitae.CurriculumVitaeDbContext context)
    {
        if (context.Companies.Any()) return; // already seeded


        var skillCSharp = new Skill { Name = "C#", Level = "Advanced" };
        var skillEf = new Skill { Name = "Entity Framework", Level = "Advanced" };
        var skillGraphQl = new Skill { Name = "GraphQL", Level = "Intermediate" };


        var company = new Company
        {
            Name = "Apator Miitors",
            Website = "https://www.Miitors.com",
            Description = "Smart metering solutions development",
            Started = new DateTime(2019, 2, 1),
            Ended = new DateTime(2023, 2, 28)
        };
        
        var education = new Education
        {
            Institution = "Aarhus University",
            Degree = "Bachelor of Science in Electrical Engineering",
            From = new DateTime(2013, 9, 1),
            To = new DateTime(2017, 1, 17),
            Description = "Studied electrical engineering with focus on wireless communication."
        };
        var education2 = new Education
        {
            Institution = "Aarhus University",
            Degree = "Master of Science in Electro Technology",
            From = new DateTime(2017, 2, 1),
            To = new DateTime(2019, 1, 14),
            Description = "Master of science and engineering, thesis on global positioning."
        };

        var project = new Project
        {
            Title = "LoRaWAN",
            Description = "Implemented a LoRaWAN stack for the smart meters to communicate with the network.",
            From = new DateTime(2022, 1, 1),
            To = new DateTime(2023, 1, 1),
            Company = company
        };

        var candidate = new Candidate
        {
            Name = "Lennart Balle",
            Description = "Candidate with experience in .NET development and a background in electrical engineering.",
            DateOfBirth = new DateTime(1992, 4, 5),
            Email = "lennartballe@gmail.com",
            
            Companies = new List<Company> { company },
            Projects = new List<Project> { project },
            Skills = new List<Skill> { skillCSharp, skillEf, skillGraphQl },
            Educations = new List<Education> { education, education2 }
        };

        context.Skills.AddRange(skillCSharp, skillEf, skillGraphQl);
        context.Companies.Add(company);
        context.Projects.Add(project);
        context.Educations.Add(education);
        context.Educations.Add(education2);
        context.Candidates.Add(candidate);

        context.SaveChanges();

        context.ProjectSkills.Add(new ProjectSkill { ProjectId = project.Id, SkillId = skillCSharp.Id });
        context.ProjectSkills.Add(new ProjectSkill { ProjectId = project.Id, SkillId = skillEf.Id });
        
        context.SaveChanges();
    }
}