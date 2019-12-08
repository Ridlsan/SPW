namespace SPW.Examples
{
	using System;

	using global::Autofac;

	using SPW.Examples.DependencyInjection;

	/// <summary>
	/// Example of usage
	/// </summary>
	internal class Program
	{
		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public static void Init()
		{
			DI.Initialize();
		}

		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		private static void Main(string[] args)
		{
			Init();
			using (var container = DI.Container.BeginLifetimeScope())
			{
				var service = container.Resolve<DynamicService>();

				// service.CreateAndUpdateItem();
				// var items = service.QueryItems();
				var typedService = container.Resolve<TypedService>();
				typedService.LinqGetItem();

				// typedService.Run();
				Console.WriteLine("End");
				Console.ReadLine();
			}
		}
	}
}