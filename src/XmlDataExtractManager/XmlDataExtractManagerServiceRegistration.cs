
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XmlDataExtractManager.Interfaces;
using XmlDataExtractManager.Services;

namespace XmlDataExtractManager
{
    public static class XmlDataExtractManagerServiceRegistration
    {
        public static IServiceCollection AddXmlDataExtractManagerServices(this IServiceCollection services)
        {            
            services.AddScoped<IXmlDataExtractorService, XmlDataExtractorService>();
            services.AddScoped<IBufferedFileUploadService, BufferedFileUploadService>();

            return services;
        }
    }
}
