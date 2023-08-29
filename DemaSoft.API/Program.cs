using DemaSoft.CodeGenerator;
using DemaSoft.CodeGenerator.Languages;
using DemaSoft.CodeGenerator.Languages.CSharp;
using DemaSoft.CodeGenerator.Languages.TypeScript;
using DemaSoft.Packager;
using DemaSoft.Packager.Publishers;
using DemaSoft.Packager.Publishers.Npm;
using DemaSoft.Packager.Publishers.Nuget;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/DemaSoft.Api.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add AWS Lambda support.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddApiVersioning(setupAction =>
    {
        setupAction.AssumeDefaultVersionWhenUnspecified = true;
        setupAction.DefaultApiVersion = new ApiVersion(1, 0);
        setupAction.ReportApiVersions = true;
    }
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Register Interfaces
// To do need to register all interfaces
builder.Services.AddTransient<ILanguageCodeGenerator, LanguageCodeGenerator>();



builder.Services.AddSingleton<ICodeGeneratorManagerFactory>(x =>
    ActivatorUtilities.CreateInstance<CodeGeneratorManagerFactory>(x, ""));
builder.Services.AddSingleton<IPackageManager>(x =>
    ActivatorUtilities.CreateInstance<PackageManagerFactory>(x, ""));

builder.Services.AddSingleton<ICodeGenerator>(x => new GenerateCode(new CodeGeneratorManagerFactory( new AssemblyGenerator()),
    new PackageManagerFactory()));

builder.Services.AddSingleton<ICSharpLanguageGenerator>(x => new CSharpLanguageGenerator(new AssemblyGenerator()));
builder.Services.AddSingleton<ITypeScriptLanguageGenerator>(x => new TypeScriptLanguageGenerator());
builder.Services.AddTransient<ITypeScriptLanguageGenerator, TypeScriptLanguageGenerator>();

builder.Services.AddTransient<IPublisher, NuGetPackageManager>();
builder.Services.AddTransient<IPublisher, NpmPackageManager>();
builder.Services.AddTransient<IProcessExecutor, ProcessExecutor>();
builder.Services.AddTransient<IProcessExecutor, ProcessExecutor>();
builder.Services.AddTransient<IPublisher, Publisher>();
 
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
    endpoints.MapControllers());


app.Run();