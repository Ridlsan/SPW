using System;
using SPW.Deployment;

namespace SPW
{
	public abstract class SwConcreteWeb : IDisposable
    {
        protected readonly ISwContext _context;
        protected readonly Lazy<ISwWeb> _sWeb;

        public SwConcreteWeb(ISwContext context)
        {
            _context = context;
            _sWeb = new Lazy<ISwWeb>(InitWeb);
        }

        public abstract SwConcreteSite Site { get; }
        public abstract string ServerRelativeUrl { get; }
        public abstract string Title { get; }
        public virtual string Description { get; }
        public virtual int CultureId => 1049;

        public void Dispose()
        {
            if (_sWeb.IsValueCreated)
            {
                _sWeb.Value?.Dispose();
            }
        }

        private ISwWeb InitWeb()
        {
            return _context.Web(ServerRelativeUrl);
        }
    }
}