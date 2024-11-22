using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Med.Infra.Orm.Compartinhado
{
    public class MedDbContextFactory: IDesignTimeDbContextFactory<MedDbContext>
    {
        public MedDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MedDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("SqlServer");

            builder.UseSqlServer(connectionString);

            return new MedDbContext(builder.Options);
        }
    }
}
