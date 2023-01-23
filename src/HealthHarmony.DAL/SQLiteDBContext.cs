using HealthHarmony.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthHarmony.DAL;

public class SQLiteDBContext : DbContext
{
    public SQLiteDBContext(DbContextOptions<SQLiteDBContext> options) : base(options)
    {
    }

    public DbSet<Record> Records { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Record>().Property(r=> r.Title).IsRequired(); 
        base.OnModelCreating(modelBuilder);
    }
}