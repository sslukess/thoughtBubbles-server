using ThoughtBubbles.Data;
using ThoughtBubbles.Services;
using ThoughtBubbles.Helpers;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
// Get allowed origins from environment:
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["localhost"];

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Configure database connection: 
if (builder.Environment.IsDevelopment())
{
    // local dev database
    builder.AddNpgsqlDbContext<ThoughtBubblesContext>("DEVELOPMENT_DATABASE");
}
else
{
     var productionDatabase = builder.Configuration["PRODUCTION_DATABASE"]; // Get from config
     Console.WriteLine(productionDatabase);
    builder.AddNpgsqlDbContext<ThoughtBubblesContext>("PRODUCTION_DATABASE");
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

// default for the base route
app.MapGet("/", () => "Hello, you have reached the ThoughtBubbles API. Please continue inside for fun and/or games!");

app.Run();


