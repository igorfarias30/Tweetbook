using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweetbook.Data;
using Tweetbook.Services;

namespace Tweetbook.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                var defaultConnection = configuration.GetSection("ConnectionStrings");
                var connectionString = defaultConnection["DefaultConnection"] ?? "Test";
                //options.UseSqlServer(connectionString);

                options.UseInMemoryDatabase(connectionString);
            });

            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IPostService, PostService>();
            services.AddSingleton<IPostService, CosmosPostService>();
        }
    }
}
