using SPW.Utils;

namespace SPW
{
    public class SwContext : ISwContext
    {
        /// <inheritdoc />
        public ISwWeb Web(string url)
        {
            return new SwWeb(new WebSiteContext(url).Web);
        }
    }

    public interface ISwContext
    {
        ISwWeb Web(string url);
    }
}