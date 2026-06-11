using API.Data;
using API.Data.Repository;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;                       // Include the "API-supported-versions" and "API-deprecated-versions" headers in responses.
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));       // Read the API version from the URL segment and the "X-Api-Version" header.
})
.AddMvc()
.AddApiExplorer(options =>                                  // Add API explorer to discover versions and generate documentation - Swagger.
{
    options.GroupNameFormat = "'v'V";                       // Format the group name as "v1", "v2", etc.
    options.SubstituteApiVersionInUrl = true;               // Substitute the API version in the URL when generating documentation.
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Simple_API_DB"), 
        sqlOptions => sqlOptions.EnableRetryOnFailure())
);

builder.Services.AddScoped<IApplicantRepository, ApplicantRepo>();
builder.Services.AddTransient<DataContextInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var init = scope.ServiceProvider.GetRequiredService<DataContextInitializer>();
    await init.InitializeAsync();
    await init.SeedDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
