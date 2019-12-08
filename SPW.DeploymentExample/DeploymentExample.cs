using System.Reflection;
using Autofac;
using SPW.ContextExample.CarsWeb;
using SPW.Deployment.Library;

namespace SPW.DeploymentExample
{
    internal class DeploymentExample
    {
        private static IContainer _di;

        private static void RegisterAutofac()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<SwContext>().As<ISwContext>();
            var contextAssembly = Assembly.GetAssembly(typeof(ContextExample.ContextExample));
            containerBuilder.RegisterAssemblyTypes(contextAssembly);
            _di = containerBuilder.Build();
        }


        private static void Main(string[] args)
        {
            RegisterAutofac();
            var serverUrl = "http://zvezd-pc187/";
            var builder = new SwFarmBuilder(serverUrl);
            builder.RegisterWeb(_di.Resolve<ProductionWeb>());
            builder.Build();
        }
    }
}