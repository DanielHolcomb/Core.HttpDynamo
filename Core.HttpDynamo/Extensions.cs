using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;

namespace Core.HttpDynamo
{
    public static class Extensions
    {
        public static void AddHttpDynamo(this IServiceCollection services)
        {
            services.AddHttpClient();
        }
    }
}