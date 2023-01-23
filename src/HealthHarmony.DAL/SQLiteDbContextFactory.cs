using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HealthHarmony.DAL;

public class SqLiteDbContextFactory : IDesignTimeDbContextFactory<SQLiteDBContext>
{
    SQLiteDBContext IDesignTimeDbContextFactory<SQLiteDBContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SQLiteDBContext>();
        
        optionsBuilder.UseSqlite("Data Source=DB/HealthHarmony.db");

        return new SQLiteDBContext(optionsBuilder.Options);
    }
}