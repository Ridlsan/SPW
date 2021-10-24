namespace SPW.Autofac
{
	using global::Autofac;

	/// <summary>
	///   Autofac module.
	/// </summary>
	/// <seealso cref="Autofac.Module" />
	public class SpwModule : Module
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SwContext>().As<ISwContext>();
		}
	}
}