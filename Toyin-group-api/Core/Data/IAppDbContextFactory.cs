namespace Toyin_group_api.Core.Data;

    /// <summary>
    /// Factory For parmeterless AppDbContext
    /// </summary>
    public interface IAppDbContextFactory
    {
        AppDbContext ResolveDbContext();
    }

