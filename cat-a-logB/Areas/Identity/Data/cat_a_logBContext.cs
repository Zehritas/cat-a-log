using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cat_a_logB.Data;

public class cat_a_logBContext : IdentityDbContext<IdentityUser>
{
    public cat_a_logBContext(DbContextOptions<cat_a_logBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Dependency> Dependency { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<GanttData> GanttData { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<ProjectMilestone> ProjectMilestone { get; set; }
    public DbSet<TaskData> TaskData { get; set; }
    public DbSet<ProjectTeam> ProjectTeam { get; set; }
}
