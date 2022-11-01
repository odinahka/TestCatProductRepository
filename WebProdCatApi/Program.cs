
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PFS.ProdCat.API.Controllers.ServiceConfigs;
using PFS.ProdCat.App.CategoryApp;
using PFS.ProdCat.App.ProductApp;
using PFS.ProdCat.DataAccess.DataAccess;
using PFS.ProdCat.Model.Common;
using PFS.ProdCat.Model.Dtos;
using PFS.ProdCat.Repository.EntityDb;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString(Attributes.ConnectionString);

// Add services to the container.
builder.Services.AddTransient(typeof(IConfigureOptions<SwaggerGenOptions>), typeof(ConfigureSwaggerGenOptions));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductApp, ProductApp>();
builder.Services.AddScoped<ICategoryApp, CategoryApp>();


builder.Services.AddAutoMapper(typeof(ProdCatMapperConfig));
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(connectionString);
});
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
                            .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors(x => x.AddPolicy("TestProdCors", p => p
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()));
builder.Services.AddEndpointsApiExplorer();

builder.VersionConfig();

var app = builder.Build();
var provider = builder.Services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
try
{
    if (app.Environment.IsDevelopment())
    {

    }
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        foreach (var desc in provider.ApiVersionDescriptions)
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
    });
    app.UseRouting();
    app.UseSerilogRequestLogging();
    app.UseCors("TestProdCors");
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor |
ForwardedHeaders.XForwardedProto
    });
    //app.UseAuthentication();
    //app.UseAuthorization();
    app.UseHttpsRedirection();

    app.MapControllers();


    logger.LogInformation("");
    logger.LogInformation("");
    logger.LogInformation("====================================================================================");
    logger.LogInformation("Starting Test ProdCat App host");
    app.Run();
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while starting the aTest ProdCat host.");
    logger.LogCritical(ex, "The Application failed to start.");
}


