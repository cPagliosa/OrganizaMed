using OrganizaMed.WebApi.Filters;

namespace OrganizaMed.WebApi
{
    public static class DependencyInjection
    {
        public static void ConfigureControllersWithFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ResponseWrapperFilter>();
            });
        }
    }
}
