namespace Toyin_group_api.Core.Services
{
    public static class ServicesBootstrapper
    {
        /// <summary>
        /// Init All Services
        /// </summary>
        /// <param name="services"></param>
        public static void InitServices(this IServiceCollection services)
        {
            AutoInjectLayers(services);
        }


        /// <summary>
        /// Scrutor Scans The Assembly For Classes To Auto Inject A Scoped Instance
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void AutoInjectLayers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromCallingAssembly().AddClasses(classes => classes
                    .Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Service")), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

    }
}
