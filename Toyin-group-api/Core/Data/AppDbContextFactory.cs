namespace Toyin_group_api.Core.Data;

    public class AppDbContextFactory : IAppDbContextFactory
    {

        /// <summary>
        /// Resolves A New Instance Of DB Context
        /// </summary>
        /// <returns></returns>
        public AppDbContext ResolveDbContext() => new AppDbContext();
    }

