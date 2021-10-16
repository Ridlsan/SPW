using Autofac;
using SPW.Examples.DependencyInjection;
using SPW.Mocks;
using SPW.Mocks.Utils;
using System;
using System.Data;
using Xunit;
using static Xunit.Assert;

namespace SPW.Examples.Tests
{
	public class TypedServiceTests
	{
		public TypedServiceTests()
		{
			var containerBuilder = new ContainerBuilder();
			var mockContext = new SwMockContext();
			mockContext.RegisterWeb(new SwMockWeb("myweb", CarsWeb));
			containerBuilder.RegisterInstance(mockContext).As<ISwContext>();
			containerBuilder.RegisterType<MyWeb>();
			containerBuilder.RegisterType<DynamicService>();
			var container = containerBuilder.Build();
			DI.Initialize(container);
		}

		private readonly DataTable Cars = DataTableUtils.CreateTable<Car>("cars");

		private DataSet CarsWeb
		{
			get
			{
				var set = new DataSet();
				set.Tables.Add(Cars);
				return set;
			}
		}

		[Fact]
		public void RunTest()
		{
			using (var container = DI.Container.BeginLifetimeScope())
			{
				var service = container.Resolve<DynamicService>();
				service.CreateAndUpdateItem();
				Equal(1, Cars.Rows.Count);
				var addedRow = Cars.Rows[0];
				Equal(DateTime.Parse("01.01.2000"), addedRow.Field<DateTime>("Produced"));
			}
		}
	}
}