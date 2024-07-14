using SimpleList.Domain.Concrete;
using SimpleList.Domain.Entities;
using SimpleList.WebUI.Domain.Repository;
using System.Data.Entity.Core.Metadata.Edm;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;

namespace SimpleList.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register repositories with Unity
            RegisterRepositories();
        }


        private void RegisterRepositories()
        {
            // Create unity container for dependecy injection
            var container = new UnityContainer();

            // Register DbContext and Repositories
            container.RegisterType<ApplicationDbContext>(); 
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>)); // Example: Generic Repository

            // Set Unity as the MVC dependency resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
