using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
	public AppDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

		// Obtém a string de conexão do arquivo appsettings.json ou de outro lugar
		var configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../API"))
		.AddJsonFile("appsettings.json")
		.Build();

		optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

		return new AppDbContext(optionsBuilder.Options);
	}
}