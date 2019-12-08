namespace SPW.Deployment
{
    public abstract class SwConcreteSite
    {
        public abstract string Url { get; }
        public abstract string Title { get; }
        public abstract string DataBaseName { get; }
    }
}