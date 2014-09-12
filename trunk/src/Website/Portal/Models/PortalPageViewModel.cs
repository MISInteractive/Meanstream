using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Models
{
    public class PortalPageViewModel
    {
        public Guid Id { get; set; }
        public Guid PortalId { get; set; }
        public Guid VersionId { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Author { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string Layout { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public bool IsEditable { get; set; }
        public bool IsPreview { get; set; }
        public bool IsVersion { get; set; }

        Dictionary<string, List<Meanstream.Portal.Core.WidgetFramework.Widget>> _widgets = new Dictionary<string, List<Meanstream.Portal.Core.WidgetFramework.Widget>>();
        public Dictionary<string, List<Meanstream.Portal.Core.WidgetFramework.Widget>> Widgets 
        {
            get
            {
                return _widgets;
            }

            set 
            {
                _widgets = value;
            } 
        }

        Dictionary<string, List<Meanstream.Portal.Core.WidgetFramework.WidgetVersion>> _widgetVersions = new Dictionary<string, List<Meanstream.Portal.Core.WidgetFramework.WidgetVersion>>();
        public Dictionary<string, List<Meanstream.Portal.Core.WidgetFramework.WidgetVersion>> WidgetVersions
        {
            get
            {
                return _widgetVersions;
            }

            set
            {
                _widgetVersions = value;
            }
        }
    }
}