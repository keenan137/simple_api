using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Data;

public class DataContextInitializer
{
    private readonly DataContext _context;

    public DataContextInitializer(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Conveniently runs migrations once the application runs.
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Seeds Database with applicant data, and related skills.
    /// </summary>
    public async Task SeedDatabaseAsync()
    {
        try
        {
            Applicant newApplicant = new()
            {
                Name = "Keenan",
                Skills = new List<Skill>
                {
                    new() { Name = "ASP.NET Core (WEB API)" },
                    new() { Name = "React & React-Native" },
                    new() { Name = "Teamwork" }
                }
            };

            _context.Applicants.Add(newApplicant);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }
}