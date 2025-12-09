using Microsoft.EntityFrameworkCore;
using Mindworking.Models.CurriculumVitae;

namespace Mindworking.Data.CurriculumVitae;
public class CurriculumVitaeDbContext : DbContext
{
    public CurriculumVitaeDbContext(DbContextOptions<CurriculumVitaeDbContext> options) : base(options) { }
    
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<ProjectSkill> ProjectSkills => Set<ProjectSkill>();
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectSkill>()
            .HasKey(ps => new { ps.ProjectId, ps.SkillId });
        
        modelBuilder.Entity<Candidate>()
            .HasMany(c => c.Companies)
            .WithOne(cmp => cmp.Candidate)
            .HasForeignKey(cmp => cmp.CandidateId);

        modelBuilder.Entity<Candidate>()
            .HasMany(c => c.Projects)
            .WithOne(p => p.Candidate)
            .HasForeignKey(p => p.CandidateId);

        modelBuilder.Entity<Candidate>()
            .HasMany(c => c.Skills)
            .WithOne(s => s.Candidate)
            .HasForeignKey(s => s.CandidateId);

        modelBuilder.Entity<Candidate>()
            .HasMany(c => c.Educations)
            .WithOne(e => e.Candidate)
            .HasForeignKey(e => e.CandidateId);

        base.OnModelCreating(modelBuilder);
    }
}