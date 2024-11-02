using ThoughtBubbles.Data;
using ThoughtBubbles.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
// Get allowed origins from environment:
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Configure database connection: 
if (builder.Environment.IsDevelopment())
{
    // local dev database
    builder.AddNpgsqlDbContext<ThoughtBubblesContext>("postgresdb");
}
else
{
    //production database
    builder.AddNpgsqlDbContext<ThoughtBubblesContext>("AZURE_POSTGRESQL_CONNECTIONSTRING");
}

builder.Services.AddScoped<ThoughtBubblesService>();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(allowedOrigins)
                                            .AllowAnyHeader() // Allow any header
                                            .AllowAnyMethod(); // Allow any method;
                      });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// use Cors
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


