using CsvHelper;
using DataImporting.Abstraction.Services;
using DataImporting.Services;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.AutoMapper;
using Organizations.Business.Factories;
using Organizations.Business.Models.Options;
using Organizations.Business.Services;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.DatabaseContexts;
using Organizations.Data.Models.Options;
using Organizations.Data.OrganizationsDatabase.Configuraters;
using Organizations.Data.OrganizationsDatabase.Repositories;
using Organizations.Presentation.API.BackgroundServices;
using Organizations.Presentation.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var organizationsDatabaseOptions = builder.Configuration.GetSection(nameof(OrganizationsDatabaseOptions));
var dataOptions = builder.Configuration.GetSection(nameof(DataOptions));
var allowedHostsOptions = builder.Configuration.GetSection(nameof(HostsOptions));

builder.Services.Configure<OrganizationsDatabaseOptions>(organizationsDatabaseOptions);
builder.Services.Configure<DataOptions>(dataOptions);
builder.Services.Configure<HostsOptions>(allowedHostsOptions);

builder.Services.AddAutoMapper(typeof(OrganizationsProfile));
builder.Services.AddTransient<IOrganizationsDatabaseConnectionValidator, OrganizationsDatabaseConnectionValidator>();
builder.Services.AddTransient<IOrganizationsDatabaseTableExistenceChecker, OrganizationsDatabaseTableExistenceChecker>();
builder.Services.AddTransient<IOrganizationsDatabaseTableInitializer, OrganizationsDatabaseTableInitializer>();
builder.Services.AddTransient<IOrganizationsDatabaseConfigurator, OrganizationsDatabaseConfigurator>();
builder.Services.AddHostedService<OrganizationsDatabaseConfigHostedService>();
builder.Services.AddTransient<IOrganizationsDatabaseCountryRepository, OrganizationsDatabaseCountryRepository>();
builder.Services.AddTransient<IOrganizationsDatabaseIndustryRepository, OrganizationsDatabaseIndustryRepository>();
builder.Services.AddTransient<IOrganizationsDatabaseOrganizationRepository, OrganizationsDatabaseOrganizationRepository>();
builder.Services.AddTransient<IOrganizationsDatabaseOrganizationIndustryRepository, OrganizationsDatabaseOrganizationIndustryRepository>();
builder.Services.AddTransient<IOrganizationsDatabaseStatisticRepository, OrganizationsDatabaseStatisticRepository>();
builder.Services.AddTransient<IAPIResultFactory, APIResultFactory>();
builder.Services.AddTransient<IOrganizationsContext, OrganizationsContext>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IIndustryService, IndustryService>();
builder.Services.AddScoped<IStatisticService, StatisticService>();

builder.Services.AddTransient<IIndustriesNormalizer, IndustriesNormalizer>();
builder.Services.AddTransient<IOrganizationDataNormalizer, OrganizationDataNormalizer>();
builder.Services.AddTransient<ICSVReader, CSVReader>();
builder.Services.AddTransient<IDataImporter, DataImporter>();
builder.Services.AddTransient<IFileNameGenerator,FileNameGenerator>();
builder.Services.AddTransient<IOrganizationsDataFileHandler, OrganizationsDataFileHandler>();
builder.Services.AddTransient<IDataImportingManager, DataImportingManager>();
builder.Services.AddHostedService<RecurringDataImportingHostedService>();

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

app.UseMiddleware<IPFilteringMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
