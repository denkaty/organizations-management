using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using Organizations.Data.Models.Options;
using Organizations.Data.OrganizationsDatabase.Configuraters;
using Organizations.Presentation.API.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

var organizationsDatabaseOptions = builder.Configuration.GetSection(nameof(OrganizationsDatabaseOptions));

builder.Services.Configure<OrganizationsDatabaseOptions>(organizationsDatabaseOptions);

builder.Services.AddTransient<IOrganizationsDatabaseConnectionValidator, OrganizationsDatabaseConnectionValidator>();
builder.Services.AddTransient<IOrganizationsDatabaseTableExistenceChecker, OrganizationsDatabaseTableExistenceChecker>();
builder.Services.AddTransient<IOrganizationsDatabaseTableInitializer, OrganizationsDatabaseTableInitializer>();
builder.Services.AddTransient<IOrganizationsDatabaseConfigurator, OrganizationsDatabaseConfigurator>();
builder.Services.AddHostedService<OrganizationsDatabaseConfigHostedService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
