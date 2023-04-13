namespace CompanyEmployees.API.ContextFactory
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Repository.Context;

    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RepositoryContextFactory()
        {
        }

        public RepositoryContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("SCB"));//Nombre del proyecto API

            return new RepositoryContext(builder.Options, _httpContextAccessor);
        }
    }
}
