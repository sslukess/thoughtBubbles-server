using ThoughtBubbles.Data;
using ThoughtBubbles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.Database;

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
    // local dev databases

    // Thought Bubbles
    string databaseString = builder.Configuration.GetConnectionString("DEVELOPMENT_DATABASE") ?? "NOSTRING";
    Console.WriteLine("!!!!!!!!!!!!!!!!!");
    Console.WriteLine("connecting to database with string:");
    Console.WriteLine(databaseString);
    Console.WriteLine("!!!!!!!!!!!!!!!!!");

    builder.Services.AddDbContext<ThoughtBubblesContext>(options =>
            options.UseNpgsql(databaseString));

    //Users
    string databaseOfUsersString = builder.Configuration.GetConnectionString("DEVELOPMENT_USER_DATABASE") ?? "NOSTRING";
    Console.WriteLine("!!!!!!!!!!!!!!!!!");
    Console.WriteLine("connecting to database with string:");
    Console.WriteLine(databaseOfUsersString);
    Console.WriteLine("!!!!!!!!!!!!!!!!!");

    builder.Services.AddDbContext<UsersDbContext>(options =>
            options.UseNpgsql(databaseOfUsersString));

}
else
{
    var PGHOST = builder.Configuration["PGHOST"];// Get from Railway config
    var PGPORT = builder.Configuration["PGPORT"];// Get from Railway config
    var DBDATABASE = builder.Configuration["PGDATABASE"];// Get from Railway config
    var PGUSER = builder.Configuration["PGUSER"];// Get from Railway config
    var PGPASSWORD = builder.Configuration["PGPASSWORD"]; // Get from Railway config

    // use dummy value for the connection string, but the override it. 
    // This is because AddNpgsqlDbContext wants to read a value from the appsettings.ConnectionStrings 
    // during start up. 
    // TODO build in null checks etc. 
    // builder.AddNpgsqlDbContext<ThoughtBubblesContext>("DUMMY_PRODUCTION_DATABASE",
    // o => o.ConnectionString =  $"Host={PGHOST};Port={PGPORT};Database={DBDATABASE};Username={PGUSER};Password={PGPASSWORD};");

    string databaseString = $"Host={PGHOST};Port={PGPORT};Database={DBDATABASE};Username={PGUSER};Password={PGPASSWORD};";
    Console.WriteLine("!!!!!!!!!!!!!!!!!");
    Console.WriteLine("connecting to database with string:");
    Console.WriteLine(databaseString);
    Console.WriteLine("!!!!!!!!!!!!!!!!!");

    builder.Services.AddDbContext<ThoughtBubblesContext>(options =>
            options.UseNpgsql(databaseString));

    //Users 
    // TODO hook this up to prod env var
    string databaseOfUsersString = builder.Configuration.GetConnectionString("DEVELOPMENT_USER_DATABASE") ?? "NOSTRING";
    Console.WriteLine("!!!!!!!!!!!!!!!!!");
    Console.WriteLine("connecting to database with string:");
    Console.WriteLine(databaseOfUsersString);
    Console.WriteLine("!!!!!!!!!!!!!!!!!");

    builder.Services.AddDbContext<UsersDbContext>(options =>
            options.UseNpgsql(databaseOfUsersString));
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

// Authentication 
builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddIdentityCore<User>()
        .AddEntityFrameworkStores<UsersDbContext>()
        .AddApiEndpoints();


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

app.MapIdentityApi<User>();

// default for the base route
app.MapGet("/", () => "Hello, you have reached the ThoughtBubbles API. Please continue inside for fun and/or games!");

app.Run();


