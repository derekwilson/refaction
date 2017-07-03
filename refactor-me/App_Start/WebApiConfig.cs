using Domain.Data;
using Domain.Repository;
using Domain.SQLServer;
using Microsoft.Practices.Unity;
using refactor_me.IoC;
using System.Web.Http;

namespace refactor_me
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Unity
			var container = new UnityContainer();
			string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["productDb"].ConnectionString;
			IConnectionFactory connectionFactory = new WebConnectionStringConnectionFactory(connectionString);
			// everybody gets the same connection factory - there is only one DB
			container.RegisterInstance<IConnectionFactory>(connectionFactory);
			container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IProductOptionRepository, ProductOptionRepository>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);

			// Web API configuration and services
			var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

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
