using ThoughtBubbles.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddNpgsqlDbContext<ThoughtBubblesContext>("postgresdb");

// Configure database connection: 
if (builder.Environment.IsDevelopment())
{
    // local dev database
    builder.AddNpgsqlDbContext<ThoughtBubblesContext>("postgresdb");
}
else
{
    builder.AddNpgsqlDbContext<ThoughtBubblesContext>("AZURE_POSTGRESQL_CONNECTIONSTRING");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
