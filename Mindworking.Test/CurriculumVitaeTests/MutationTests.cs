using Microsoft.EntityFrameworkCore;
using Mindworking.Data.CurriculumVitae;
using Mindworking.GraphQl.CurriculumVitae;
using Mindworking.Models.CurriculumVitae;
using Xunit;
using Assert = Xunit.Assert;

namespace Mindworking.Test.CurriculumVitaeTests
{
    public class MutationTests
    {
        private static CurriculumVitaeDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CurriculumVitaeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new CurriculumVitaeDbContext(options);
        }

        [Fact]
        public async Task AddCandidateAsync_AddsCandidateToDatabase()
        {
            // Arrange
            await using var db = CreateInMemoryContext();
            var mutation = new Mutation();
            var input = new AddCandidateInput("Test User", "desc", new DateTime(1990,1,1), "test@example.com");

            // Act
            var result = await mutation.AddCandidateAsync(input, db);
            var stored = await db.Candidates.FindAsync(result.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test User", result.Name);
            
            Assert.NotNull(stored);
            Assert.Equal("test@example.com", stored.Email);
        }
        
        [Fact]
        public async Task UpdateCandidateAsync_UpdatesFields()
        {
            // Arrange
            await using var db = CreateInMemoryContext();
            var candidate = new Candidate { Name = "Initial", Email = "a@b.com" };
            db.Candidates.Add(candidate);
            await db.SaveChangesAsync();

            var mutation = new Mutation();
            var update = new UpdateCandidateInput(candidate.Id, "Updated", "new desc", new DateTime(2000,1,1), "updated@ex.com");
            // Act
            var updated = await mutation.UpdateCandidateAsync(update, db);
            // Assert
            Assert.NotNull(updated);
            Assert.Equal("Updated", updated.Name);
            Assert.Equal("updated@ex.com", updated.Email);
        }

        [Fact]
        public async Task DeleteCandidateAsync_RemovesCandidate()
        {
            // Arrange
            await using var db = CreateInMemoryContext();
            var candidate = new Candidate { Name = "ToDelete" };
            db.Candidates.Add(candidate);
            await db.SaveChangesAsync();

            var mutation = new Mutation();
            // Act
            var deleted = await mutation.DeleteCandidateAsync(candidate.Id, db);
            var found = await db.Candidates.FindAsync(candidate.Id);
            // Assert
            Assert.True(deleted);
            Assert.Null(found);
        }

        [Fact]
        public async Task AddProjectAsync_AssociatesWithCompany()
        {
            // Arrange
            await using var db = CreateInMemoryContext();
            var candidate = new Candidate { Name = "C1" };
            var company = new Company { Name = "Co1", Candidate = candidate };
            db.Candidates.Add(candidate);
            db.Companies.Add(company);
            await db.SaveChangesAsync();

            var mutation = new Mutation();
            // Act
            var input = new AddProjectInput(candidate.Id, company.Id, "P1", "desc", DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow);
            var project = await mutation.AddProjectAsync(input, db);
            
            var stored = await db.Projects.FindAsync(project.Id);
            
            // Assert
            Assert.NotNull(project);
            Assert.Equal(company.Id, project.CompanyId);
            
            Assert.NotNull(stored);
            Assert.Equal("P1", stored.Title);
            Assert.Equal(company.Id, stored.CompanyId);
        }

        [Fact]
        public async Task AssignAndRemoveSkillToProject_Works()
        {
            // Arrange
            await using var db = CreateInMemoryContext();
            var candidate = new Candidate { Name = "C2" };
            db.Candidates.Add(candidate);
            await db.SaveChangesAsync();

            var project = new Project { Title = "Proj", CandidateId = candidate.Id };
            var skill = new Skill { Name = "X", CandidateId = candidate.Id };
            db.Projects.Add(project);
            db.Skills.Add(skill);
            await db.SaveChangesAsync();
            
            var mutation = new Mutation();
            // Act
            var assigned = await mutation.AssignSkillToProjectAsync(project.Id, skill.Id, db);

            var ps = db.ProjectSkills.SingleOrDefault(x => x.ProjectId == project.Id && x.SkillId == skill.Id);
            var removed = await mutation.RemoveSkillFromProjectAsync(project.Id, skill.Id, db);
            var psAfter = await db.ProjectSkills.FindAsync(project.Id, skill.Id);
            
            // Assert
            Assert.True(assigned);
            Assert.NotNull(ps);
            Assert.True(removed);
            Assert.Null(psAfter);
        }
    }
}