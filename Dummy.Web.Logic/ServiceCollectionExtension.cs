namespace Dummy.Web.Logic
{
    using Dummy.Web.DataAccess;
    using Dummy.Web.Repository.User;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddModels(this IServiceCollection services, string connectionString)
        {
            AddRepositoryService(services, connectionString);
            //services.AddTransient<IUserLogic, DummyClassLogic>();
        }

        private static void AddRepositoryService(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDapperHelper>(new DapperHelper(connectionString));
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
