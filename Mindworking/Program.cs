using Mindworking.GraphQl.CurriculumVitae;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.EntityFrameworkCore;
using Mindworking.Data.CurriculumVitae;
using Mindworking.DataSeed.CurriculumVitae;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<CurriculumVitaeDbContext>(options =>
{
    options.UseSqlite("Data Source=Data/CurriculumVitae.db");
}); 

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections() // allow IQueryable projections
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

Directory.CreateDirectory("Data");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbFactory = services.GetRequiredService<IDbContextFactory<CurriculumVitaeDbContext>>();
        using var context = dbFactory.CreateDbContext();
        context.Database.EnsureCreated();
        DbSeeder.Seed(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.MapGraphQL();

app.UsePlayground(new PlaygroundOptions
{
    QueryPath = "/graphql",
    Path = "/playground"
});

app.MapGet("/", () => Results.Redirect("/playground"));

app.RunWithGraphQLCommands(args);