using Data.Repository;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLResolvers;
using Microsoft.EntityFrameworkCore;
using WebXmlApp.Services;
using XmlDataExtractManager;

namespace WebXmlApp
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {      
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddGraphQLServices();
            builder.Services.AddXmlDataExtractManagerServices();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton(c => new GraphQLHttpClient(builder.Configuration["GraphQLApiUri"], new NewtonsoftJsonSerializer()));
            builder.Services.AddSingleton<XmlDataGraphClient>();
            builder.Services.AddHttpClient<XmlDataHttpClient>(c => 
            c.BaseAddress = new Uri(builder.Configuration["GraphQLApiUri"])
            );

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapGraphQL();

            return app;
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<XmlImporterDbContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
