using Microsoft.EntityFrameworkCore;
using Xunit;
using Mindworking.Data.CurriculumVitae;
using Mindworking.GraphQl.CurriculumVitae;
using Assert = Xunit.Assert;

namespace Mindworking.Test.CurriculumVitaeTests
{
    public class ValidationTests
    {
        private static CurriculumVitaeDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CurriculumVitaeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new CurriculumVitaeDbContext(options);
        }

        [Fact]
        public async Task AddCompany_ForNonExistingCandidate_ReturnsNullOrThrows()
        {
            // Arrange
            await using var db = CreateInMemoryContext();
            var mutation = new Mutation();
            var input = new AddCompanyInput(9999, "NoCandidateCo", null, null, null, null);
            
            // Act // Assert
            try
            {
                var result = await mutation.AddCompanyAsync(input, db);
                Assert.True(result is not { CandidateId: 9999 }, "Expected no association to non-existing candidate");
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
        }

        [Fact]
        public async Task AddEducation_WithMissingCandidateId_FailsGracefully()
        {
            // Arrange
            await using var db = CreateInMemoryContext();
            var mutation = new Mutation();
            var input = new AddEducationInput(12345, "Inst", "Degree", null, null, "desc");
            // Act // Assert
            try
            {
                var result = await mutation.AddEducationAsync(input, db);
                Assert.True(result is not { CandidateId: 12345 }, "Expected failure when candidate does not exist");
            }
            catch (Exception e)
            {
                Assert.True(e is ArgumentException or InvalidOperationException, "Expected specific exception types");
            }
        }
    }
}
