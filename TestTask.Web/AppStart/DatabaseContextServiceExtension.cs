using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.DAL.DataBase;

namespace TestTask.Web.AppStart
{
    public static class DatabaseContextServiceExtension
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DbContextTestTask>(options => options.UseSqlite("Data Source=" +
                                                                                     Path.Combine(Directory.GetCurrentDirectory(), "Data\\" + connection)));
        }
    }
}
