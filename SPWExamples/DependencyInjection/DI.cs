using Autofac;

namespace SPW.Examples.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static class DI
    {
        private static readonly object Lock = new object();

        /// <summary>
        ///     Return the current instance of the container to resolve types
        /// </summary>
        public static IContainer Container { get; private set; }

        /// <summary>
        ///     Initialize the container, can be call only once
        /// </summary>
        /// <param name="container"></param>
        public static void Initialize(IContainer container)
        {
            if (Container == null)
            {
                Container = container;
            }
        }

        public static void Initialize()
        {
            if (Container == null)
            {
                lock (Lock)
                {
                    if (Container == null)
                    {
                        var containerBuilder = new ContainerBuilder();
                        containerBuilder.RegisterType<SwContext>().As<ISwContext>();
                        containerBuilder.RegisterType<DynamicService>();
                        containerBuilder.RegisterType<TypedService>();
                        var container = containerBuilder.Build();
                        Initialize(container);
                    }
                }
            }
        }
    }
}