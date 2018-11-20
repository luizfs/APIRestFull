using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TreinaWeb.MinhaApi.Api.Filters;
using TreinaWeb.MinhaApi.Api.Formatters;

namespace TreinaWeb.MinhaApi.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web
            var Jsonformatter = config.Formatters.JsonFormatter;
            Jsonformatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
            config.Formatters.Add(new CsvMediaTyperFormatter());
            //var xmlformatter = config.Formatters.XmlFormatter;
            //config.Formatters.Remove(xmlformatter);

            config.Filters.Add(new FillResponseWithHATEOSAttribute());

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
