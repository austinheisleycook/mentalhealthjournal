using Microsoft.EntityFrameworkCore;

namespace healthjournal.Models;

public class HealthConfig : DbContext
{
    public HealthConfig(DbContextOptions<HealthConfig> options) : base(options)
    {
        
    }

    public DbSet<HealthModel> HealthModels { get; set; }
}
