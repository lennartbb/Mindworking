using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Mindworking.Data.CurriculumVitae;
using Mindworking.GraphQl.CurriculumVitae;
using Mindworking.Models.CurriculumVitae;
using Xunit;
using Assert = Xunit.Assert;

namespace Mindworking.Test.CurriculumVitaeTests
{
    public class SqliteIntegrationTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<CurriculumVitaeDbContext> _options;

        public SqliteIntegrationTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<CurriculumVitaeDbContext>()
                .UseSqlite(_connection)
                .Options;

            // create schema
            using var context = new CurriculumVitaeDbContext(_options);
            context.Database.EnsureCreated();
        }

        [Fact]
        public void CompositeKeyAndRelations_WorkWithSqlite()
        {
            using var db = new CurriculumVitaeDbContext(_options);

            // seed candidate, project and skill
            var candidate = new Candidate { Name = "C" };
            db.Candidates.Add(candidate);
            db.SaveChanges();

            var project = new Project { Title = "P", CandidateId = candidate.Id };
            var skill = new Skill { Name = "S", CandidateId = candidate.Id };
            db.Projects.Add(project);
            db.Skills.Add(skill);
            db.SaveChanges();

            var mutation = new Mutation();

            // call the mutation that creates ProjectSkill association
            var assigned = mutation.AssignSkillToProjectAsync(project.Id, skill.Id, db).GetAwaiter().GetResult();
            Assert.True(assigned);

            // verify composite key row exists
            var ps = db.ProjectSkills.Find(project.Id, skill.Id);
            Assert.NotNull(ps);
            Assert.Equal(project.Id, ps.ProjectId);
            Assert.Equal(skill.Id, ps.SkillId);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}