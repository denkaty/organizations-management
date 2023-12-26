using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.AutoMapper;
using Organizations.Business.Factories;
using Organizations.Business.Services;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.DatabaseContexts;
using Organizations.Data.Models.Options;
using Organizations.Data.OrganizationsDatabase.Configuraters;
using Organizations.Data.OrganizationsDatabase.Repositories;
using Organizations.Presentation.API.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

var organizationsDatabaseOptions = builder.Configuration.GetSection(nameof(OrganizationsDatabaseOptions));

builder.Services.Configure<OrganizationsDatabaseOptions>(organizationsDatabaseOptions);

builder.Services.AddAutoMapper(typeof(OrganizationsProfile));
builder.Services.AddTransient<IOrganizationsDatabaseConnectionValidator, OrganizationsDatabaseConnectionValidator>();
builder.Services.AddTransient<IOrganizationsDatabaseTableExistenceChecker, OrganizationsDatabaseTableExistenceChecker>();
builder.Services.AddTransient<IOrganizationsDatabaseTableInitializer, OrganizationsDatabaseTableInitializer>();
builder.Services.AddTransient<IOrganizationsDatabaseConfigurator, OrganizationsDatabaseConfigurator>();
builder.Services.AddHostedService<OrganizationsDatabaseConfigHostedService>();
builder.Services.AddScoped<IOrganizationsDatabaseCountryRepository, OrganizationsDatabaseCountryRepository>();
builder.Services.AddScoped<IOrganizationsDatabaseIndustryRepository, OrganizationsDatabaseIndustryRepository>();
builder.Services.AddScoped<IOrganizationsDatabaseOrganizationRepository, OrganizationsDatabaseOrganizationRepository>();
builder.Services.AddScoped<IOrganizationsDatabaseOrganizationIndustryRepository, OrganizationsDatabaseOrganizationIndustryRepository>();
builder.Services.AddScoped<IAPIResultFactory, APIResultFactory>();
builder.Services.AddScoped<IOrganizationsContext, OrganizationsContext>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();

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
