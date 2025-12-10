using Microsoft.AspNetCore.Builder;
using MyApp.API.EndPoints;

namespace MyApp.API.Class
{
    public static class EndpointExtensions
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
            ProdcutEndpoints.MapEndpoints(app);
        }
    }
}
