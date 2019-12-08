using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace SPW.Utils
{
    public class WebSiteContext : IDisposable
    {
        /// <summary>
        ///     Создает контекст из SPContext
        /// </summary>
        public WebSiteContext()
        {
            CreateSites(SPContext.Current.Site.ID, SPContext.Current.Web.ID);
        }

        /// <summary>
        ///     Создает контекст из SPContext
        /// </summary>
        public WebSiteContext(SPWeb sWeb)
        {
            CreateSites(sWeb.Site.ID, sWeb.ID);
        }

        /// <summary>
        ///     Создает контекст на основе переданных ID семейства и узла
        /// </summary>
        /// <param name="siteId">ИД Семейства.</param>
        public WebSiteContext(Guid siteId)
        {
            CreateSites(siteId);
        }

        /// <summary>
        ///     Создает контекст на основе переданных ID семейства и узла
        /// </summary>
        /// <param name="siteId">ИД Семейства.</param>
        /// <param name="webId">ИД веба.</param>
        public WebSiteContext(Guid siteId, Guid webId)
        {
            CreateSites(siteId, webId);
        }

        /// <summary>
        ///     Создает контекст на основе URL адреса узла
        /// </summary>
        /// <param name="webUrl">Урл узла. Если относительный узел, то из конфига добавится корень</param>
        public WebSiteContext(string webUrl)
        {
            var fullUrl = webUrl;
            if (!webUrl.StartsWith(Config.SiteUrl, StringComparison.InvariantCulture))
            {
                if (!webUrl.StartsWith("/"))
                {
                    fullUrl = "/" + fullUrl;
                }

                fullUrl = Config.SiteUrl + fullUrl;
            }

            this.Site = new SPSite(fullUrl);
            this.Web = Site.OpenWeb();
            this.Web.AllowUnsafeUpdates = true;
        }

        /// <summary>
        ///     Семейство под привелегиями
        /// </summary>
        public SPSite Site { get; private set; }

        /// <summary>
        ///     Узел под привилегиями
        /// </summary>
        public SPWeb Web { get; private set; }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void CreateSites(Guid siteId, Guid webId)
        {
            this.Site = new SPSite(siteId, SPUrlZone.Default);
            this.Web = Site.OpenWeb(webId);
            this.Web.AllowUnsafeUpdates = true;
        }

        private void CreateSites(Guid siteId)
        {
            this.Site = new SPSite(siteId, SPUrlZone.Default);
            this.Web = Site.RootWeb;
            this.Web.AllowUnsafeUpdates = true;
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Site?.Dispose();
                this.Web?.Dispose();
            }
        }
    }
}