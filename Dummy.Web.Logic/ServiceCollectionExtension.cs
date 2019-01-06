namespace Dummy.Web.Logic
{
    using Dummy.Web.DataAccess;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddModels(this IServiceCollection services, string connectionString)
        {
            AddRepositoryService(services, connectionString);
            //services.AddTransient<IDummyClassLogic, DummyClassLogic>();
        }

        private static void AddRepositoryService(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDapperHelper>(new DapperHelper(connectionString));
            //services.AddTransient<IDummyRepository, IDummyRepository>();
        }
    }
}
