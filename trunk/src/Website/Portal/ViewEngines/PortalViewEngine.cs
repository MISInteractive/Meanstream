using Meanstream.Portal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.ViewEngines
{
    public class PortalViewEngine : RazorViewEngine
    {
        /// <summary>
		/// Adds view locations to razor view engine
		/// </summary>
        public PortalViewEngine()
		{
            base.ViewLocationFormats = base.ViewLocationFormats.Concat(new string[] { "~/controls/portals/0/skins/{0}.cshtml" }).ToArray();
		}
    }
}