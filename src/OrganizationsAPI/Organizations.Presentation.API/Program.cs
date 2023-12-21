using Organizations.Data;
using Organizations.Data.Abstraction;
using Organizations.Presentation.API.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

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
