namespace PFS.ProdCat.API.Controllers.ServiceConfigs
{
    public static class VersioningConfiguration
    {
        public static void VersionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
        }
    }
}
