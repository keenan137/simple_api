using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    // DbSet Properties:
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Skill> Skills { get; set; }
}
