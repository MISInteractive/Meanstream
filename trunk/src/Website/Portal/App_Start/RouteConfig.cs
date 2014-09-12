using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Portal Content
            routes.MapRoute(
                "PortalContent", // Route name
                "{url}", // URL with parameters
                new { lang = "en", controller = "Portal", action = "Page" } // Parameter defaults
            );

            routes.MapRoute(
                "PortalContent2", // Route name
                "", // URL with parameters
                new { lang = "en", controller = "Portal", action = "Page" } // Parameter defaults
            );

            // Portal Preview Page
            routes.MapRoute(
                "PortalEditPage", // Route name
                "edit/page/{versionId}", // URL with parameters
                new { lang = "en", controller = "Portal", action = "EditPage" } // Parameter defaults
            );

            // Portal Preview Page
            routes.MapRoute(
                "PortalPreviewPage", // Route name
                "preview/page/{versionId}", // URL with parameters
                new { lang = "en", controller = "Portal", action = "PreviewPage" } // Parameter defaults
            );

            // Portal Skin Preview
            routes.MapRoute(
                "PortalPreviewSkin", // Route name
                "preview/skin/{skinId}", // URL with parameters
                new { lang = "en", controller = "Portal", action = "PreviewSkin" } // Parameter defaults
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
