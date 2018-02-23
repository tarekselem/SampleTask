using Futura.Services.API.Filters;
using Futura.Services.API.Handlers;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace Futura.Services.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           

            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            string allowedCorsAttributes = WebConfigurationManager.AppSettings["CorsAttributes"];
            var corsAttr = new EnableCorsAttribute(allowedCorsAttributes, "*", "*");
            config.EnableCors(corsAttr);

            config.Filters.Add(new GlobalExceptionFilterAttribute());
            config.Filters.Add(new LoggingFilterAttribute());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
