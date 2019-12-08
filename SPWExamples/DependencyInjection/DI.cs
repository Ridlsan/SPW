namespace SPW.Examples.DependencyInjection
{
	using global::Autofac;

	using SPW.Autofac;
	using SPW.Utils;

	/// <summary>
	/// The di.
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public static class DI
	{
		/// <summary>
		/// The lock
		/// </summary>
		private static readonly object Lock = new object();

		/// <summary>
		///   Return the current instance of the container to resolve types
		/// </summary>
		public static IContainer Container { get; private set; }

		/// <summary>
		/// Initialize the container, can be call only once
		/// </summary>
		/// <param name="container">The container.</param>
		public static void Initialize(IContainer container)
		{
			if (Container == null)
			{
				Container = container;
			}
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public static void Initialize()
		{
			if (Container == null)
			{
				lock (Lock)
				{
					if (Container == null)
					{
						var containerBuilder = new ContainerBuilder();
						containerBuilder.RegisterModule<SpwModule>();
						containerBuilder.RegisterType<ConfigProvider>().As<ISwConfigProvider>();
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