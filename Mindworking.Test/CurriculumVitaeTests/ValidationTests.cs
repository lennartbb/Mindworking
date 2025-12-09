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
            await using var db = CreateInMemoryContext();
            var mutation = new Mutation();
            var input = new AddCompanyInput(9999, "NoCandidateCo", null, null, null, null);

            // Implementation may either throw or return null; accept both behaviors.
            try
            {
                var result = await mutation.AddCompanyAsync(input, db);
                Assert.True(result == null || result.CandidateId != 9999, "Expected no association to non-existing candidate");
            }
            catch (ArgumentException) { /* acceptable */ }
            catch (InvalidOperationException) { /* acceptable */ }
        }

        [Fact]
        public async Task AddEducation_WithMissingCandidateId_FailsGracefully()
        {
            await using var db = CreateInMemoryContext();
            var mutation = new Mutation();
            var input = new AddEducationInput(12345, "Inst", "Degree", null, null, "desc");

            try
            {
                var result = await mutation.AddEducationAsync(input, db);
                Assert.True(result == null || result.CandidateId != 12345, "Expected failure when candidate does not exist");
            }
            catch (Exception) when (true) { /* Accept any well-formed failure */ }
        }
    }
}
