using System.Text;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigurarServices(builder);

ConfigurarInjecaoDeDependencia(builder);

var app = builder.Build();

ConfigurarAplicacao(app);

app.Run();

// Metodo que configrua as injeções de dependencia do projeto.
static void ConfigurarInjecaoDeDependencia(WebApplicationBuilder builder)
{
	builder.Services.AddSingleton(builder.Configuration)
	.AddSingleton(builder.Environment)
	.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")))
	.AddAutoMapper(typeof(ContratanteProfile).Assembly)
	.AddAutoMapper(typeof(FreelancerProfile).Assembly)
	.AddAutoMapper(typeof(ProjetoProfile).Assembly)
	.AddAutoMapper(typeof(PropostaProfile).Assembly)
	.AddAutoMapper(typeof(UsuarioProfile).Assembly)
	.AddScoped<TokenService>()
	.AddScoped<AuthService>()
	.AddScoped<ContratanteService>()
	.AddScoped<ProjetoService>()
	.AddScoped<FreelancerService>()
	.AddScoped<PropostaService>()
	.AddScoped<UsuarioService>()
	.AddScoped<UsuarioRepository>()
	.AddScoped<IContratanteRepository, ContratanteRepository>()
	.AddScoped<IFreelancerRepository, FreelancerRepository>()
	.AddScoped<IPropostaRepository, PropostaRepository>()
	.AddScoped<IUsuarioRepository, UsuarioRepository>()
	.AddScoped<IProjetoRepository, ProjetoRepository>();
}

// Configura o serviços da API.
static void ConfigurarServices(WebApplicationBuilder builder)
{
	builder.Services.AddCors()
	.AddEndpointsApiExplorer()
	.AddControllers()
	.ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; }).AddNewtonsoftJson();

	builder.Services.AddSwaggerGen(c =>
	{
		c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
		{
		Description = "JTW Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
		});

		c.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
		{
		new OpenApiSecurityScheme
		{
		Reference = new OpenApiReference
		{
		Type = ReferenceType.SecurityScheme,
		Id = "Bearer"
		}
		},
		Array.Empty<string>()
		}
		});

		c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarketplaceFreelance.Api", Version = "v1" });
	});

	builder.Services.AddAuthentication(x =>
	{
		x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(x =>
	{
		x.RequireHttpsMetadata = false;
		x.SaveToken = true;

		var secretKey = builder.Configuration["KeySecret"];
		if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException("KeySecret não pode ser nulo ou vazio!");
		x.TokenValidationParameters = new TokenValidationParameters
		{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["KeySecret"]!)),
		ValidateIssuer = false,
		ValidateAudience = false
		};
	});


	builder.Services.AddAuthorization(options =>
	{
		options.AddPolicy("ContratantePolicy", policy => policy.RequireClaim("UserType", "Contratante"));
		options.AddPolicy("FreelancerPolicy", policy => policy.RequireClaim("UserType", "Freelancer"));
	});
}

// Configura os serviços na aplicação.
static void ConfigurarAplicacao(WebApplication app)
{
	app.UseDeveloperExceptionPage()
	.UseRouting();

	app.UseStaticFiles();
	app.UseSwagger()
	.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarketplaceFreelance.Api v1");
		c.RoutePrefix = string.Empty;
		c.InjectStylesheet("/swagger-ui/dark-mode.css"); // Adiciona um CSS customizado
	});

	app.UseCors(x => x
	.AllowAnyOrigin() // Permite todas as origens
	.AllowAnyMethod() // Permite todos os métodos
	.AllowAnyHeader()) // Permite todos os cabeçalhos
	.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.MapControllers();
}