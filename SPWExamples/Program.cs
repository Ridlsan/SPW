using System;
using Autofac;
using SPW;
using SPW.Examples.Context;
using SPW.Examples.DependencyInjection;

namespace SPW.Examples
{
    internal class Program
    {
        public static void Init()
        {
            DI.Initialize();
        }


        private static void Main(string[] args)
        {
            Config.SiteUrl = "http://zvezd-pc187/";
            Init();
            using (var container = DI.Container.BeginLifetimeScope())
            {
                var service = container.Resolve<DynamicService>();
                //service.CreateAndUpdateItem();
                //var items = service.QueryItems();
                var typedService = container.Resolve<TypedService>();
                typedService.LinqGetItem();
                //typedService.Run();

                Console.WriteLine("End");
                Console.ReadLine();
            }
        }
    }
}