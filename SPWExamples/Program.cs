using Autofac;
using SPW.Examples.DependencyInjection;
using System;

namespace SPW.Examples
{

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
			using (var diContext = DI.Container.BeginLifetimeScope())
			{
				var service = diContext.Resolve<DynamicService>();
				// service.CreateAndUpdateItem();
				// var items = service.QueryItems();
				//		var typedService = container.Resolve<TypedService>();
				//	typedService.LinqGetItem();

				// typedService.Run();
				Console.WriteLine("End");
				Console.ReadLine();
			}
		}
	}
}