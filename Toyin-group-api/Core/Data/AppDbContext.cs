using Microsoft.EntityFrameworkCore;
using EntityFramework.Exceptions.Common;
using Toyin_group_api.Core.Entities;

namespace Toyin_group_api.Core.Data;

/// <summary>
/// Application Db context for database access
/// </summary>
public class AppDbContext : DbContext
{
    private readonly DbContextOptions<AppDbContext> dco;

    public AppDbContext()
    {
    }

    /// <summary>
    /// For DI - Instantiates Db Context Using Context Options
    /// </summary>
    /// <param name="dco"></param>
    public AppDbContext(DbContextOptions<AppDbContext> dco) : base(dco)
    {
     
    }

    public DbSet<Todo> Todos { get; set; }

    public async Task<int> TrySaveChangesAsync(ILogger logger, CancellationToken ct = default)
    {
        try
        {
            await SaveChangesAsync(ct);
            return DatabaseResponseCodes.Success;
        }
        catch (UniqueConstraintException e)
        {
            logger.LogError($"DB Unique Constraint Exception Message >>>>> {e.Message}");
            logger.LogError($"DB Unique Constraint Inner Exception Message >>>>> {e.InnerException?.Message}");
            return DatabaseResponseCodes.Duplicate;
        }
        catch (DbUpdateException e)
        {
            logger.LogError($"DB Update Exception Message >>>>> {e.Message}");
            logger.LogError($"DB Update Inner Exception Message >>>>> {e.InnerException?.Message}");
            return DatabaseResponseCodes.Error;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    
        if (dco is not null)
        {
            base.OnConfiguring(optionsBuilder);
            return;
        }
        optionsBuilder.UseInMemoryDatabase("toyinDb");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // use case sensitive collation, to prevent the use of ToLower and enhance index usage.. for SQL Server. 


    }



}

