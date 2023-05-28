using Microsoft.EntityFrameworkCore;
using Tasks.Models;

namespace Tasks.Data;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("DataSource=app;Cache=Shared");
}