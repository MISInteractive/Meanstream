using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Models;
using Meanstream.Portal.Core.WidgetFramework;
using Meanstream.Portal.Core;
using System.Configuration;
using Meanstream.Portal.Core.Instrumentation;
using System.Web.Caching;
using System.Web.Security;

namespace Portal.Controllers
{
    public class PortalController : Controller
    {
        public ActionResult Page(string url)
        {
            if (string.IsNullOrEmpty(url))
                url = "home";

            //get page from viewname/url
            bool allowCaching = true;
            bool viewFlag = false;
            Guid portalId = Meanstream.Portal.Core.PortalContext.Current.PortalId;
            Meanstream.Portal.Core.Content.Page page = Meanstream.Portal.Core.Content.ContentService.Current.GetPageByUrl(portalId, url);
 
            //bad request
            if (page == null) 
                return View();

            //bad request
            if (page.IsDeleted)
                FormsAuthentication.RedirectToLoginPage();

            Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.AspnetRoles> roles = null;
            Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamPagePermission> pagePermissions = GetPagePermission(page.Id, allowCaching);
            Meanstream.Portal.Core.Membership.Role AllUsersRole = GetAllUsersRole(allowCaching);

            if (Request.IsAuthenticated)
            {
                roles = GetUserRoleCache(System.Web.HttpContext.Current.Profile.UserName, allowCaching);
                foreach (Meanstream.Portal.Core.Entities.AspnetRoles role in roles)
                {
                    if (HasViewPagePermissions(pagePermissions, role.RoleId))
                    {
                        viewFlag = true;
                        break; 
                    }
                }
            }
            else
            {
                if (HasViewPagePermissions(pagePermissions, AllUsersRole.Id))
                {
                    viewFlag = true;
                }
            }

            //unauthorized
            if (!viewFlag)
                FormsAuthentication.RedirectToLoginPage();

            PortalPageViewModel model = new PortalPageViewModel();
            model.Id = page.Id;
            model.PortalId = page.PortalId;
            model.VersionId = page.VersionId;
            model.Layout = page.Skin.Path;
            model.MetaDescription = page.MetaDescription;
            model.MetaKeywords = page.MetaKeywords;
            model.MetaTitle = page.MetaTitle;
            model.Author = page.Author;
            model.EndDate = page.EndDate;
            model.PublishDate = page.PublishDate;
            model.StartDate = page.StartDate;
            model.Url = page.Url;
            model.Name = page.Name;

            foreach (Meanstream.Portal.Core.Content.SkinZone zone in page.Skin.Zones) 
            {
               //get widgets for zone and sort
                List<Widget> zoneWidgets = new List<Widget>();
                IEnumerable<Widget> widgets = page.Widgets.Where(w => w.SkinPaneId == zone.Id);
                widgets = from widget in widgets orderby widget.DisplayOrder select widget;

                foreach (Widget widget in widgets) 
                {
                    viewFlag = false;

                    Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamModulePermission> widgetPermissions = GetWidgetPermissions(widget.Id);

                    if (Request.IsAuthenticated)
                    {
                        foreach (Meanstream.Portal.Core.Entities.AspnetRoles role in roles) 
                        {
                            if (HasViewModulePermissions(widgetPermissions, widget.Id, role.RoleId)) 
                            {
                                viewFlag = true;
                                break;
                            }
                        }
                    }
                    else 
                    {
                        if (HasViewModulePermissions(widgetPermissions, widget.Id, AllUsersRole.Id))
                        {
                            viewFlag = true;
                        }
                    }

                    if (viewFlag) 
                    {
                        //add to widget zone list
                        zoneWidgets.Add(widget);
                    }
                }

                model.Widgets.Add(zone.Pane, zoneWidgets);
            }

            model.IsVersion = false;
            model.IsPreview = false;
            model.IsEditable = false;

            return View(model.Layout, model);
        }

        public ActionResult PreviewPage(string versionId)
        {
            bool viewFlag = false;
            bool editFlag = true;

            Meanstream.Portal.Core.Content.PageVersion page = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(new Guid(versionId));

            if (page == null)
                return View();

            List<Meanstream.Portal.Core.Membership.Role> roles = null;
            Meanstream.Portal.Core.Membership.Role AllUsersRole = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS);

            if (Request.IsAuthenticated)
            {
                roles = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRolesForUser(HttpContext.Profile.UserName);

                foreach (Meanstream.Portal.Core.Membership.Role Role in roles)
                {
                    if (HasViewPagePermissionsVersion(page.Id, Role.Id))
                    {
                        viewFlag = true;
                    }

                    if (HasEditPagePermissionsVersion(page.Id, Role.Id) | Meanstream.Portal.Core.Membership.MembershipService.Current.IsUserInRole(HttpContext.Profile.UserName, Meanstream.Portal.Core.AppConstants.ADMINISTRATOR))
                    {
                        editFlag = true;
                    }
                }
            }
            else
            {
                viewFlag = false;
                editFlag = false;
            }

            if (viewFlag == false)
            {
                //go to error page
                FormsAuthentication.RedirectToLoginPage();
            }

            PortalPageViewModel model = new PortalPageViewModel();
            model.Id = page.PageId;
            model.PortalId = page.PortalId;
            model.VersionId = page.Id;
            model.Layout = page.Skin.Path;
            model.MetaDescription = page.MetaDescription;
            model.MetaKeywords = page.MetaKeywords;
            model.MetaTitle = page.MetaTitle;
            model.Author = page.Author;
            model.EndDate = page.EndDate;
            model.StartDate = page.StartDate;
            model.Url = page.Url;
            model.Name = page.Name;

            foreach (Meanstream.Portal.Core.Content.SkinZone zone in page.Skin.Zones)
            {
                //get widgets for zone and sort
                List<WidgetVersion> zoneWidgets = new List<WidgetVersion>();
                IEnumerable<WidgetVersion> widgets = page.Widgets.Where(w => w.SkinPaneId == zone.Id);
                widgets = from widget in widgets orderby widget.DisplayOrder select widget;

                foreach (WidgetVersion widget in widgets)
                {
                    viewFlag = false;
                    editFlag = false;

                    if (Request.IsAuthenticated)
                    {
                        foreach (Meanstream.Portal.Core.Membership.Role role in roles)
                        {
                            if (HasViewModulePermissionsVersion(widget.Id, role.Id))
                            {
                                viewFlag = true;
                            }

                            if (HasEditModulePermissionsVersion(widget.Id, role.Id) | Meanstream.Portal.Core.Membership.MembershipService.Current.IsUserInRole(HttpContext.Profile.UserName, Meanstream.Portal.Core.AppConstants.ADMINISTRATOR))
                            {
                                editFlag = true;
                            }
                        }
                    }
                    else
                    {
                        if (HasViewModulePermissionsVersion(widget.Id, AllUsersRole.Id))
                        {
                            viewFlag = true;
                        }
                    }

                    if (viewFlag)
                    {
                        //add to widget zone list
                        zoneWidgets.Add(widget);
                    }
                }

                model.WidgetVersions.Add(zone.Pane, zoneWidgets);
            }

            model.IsPreview = true;
            model.IsEditable = false;
            model.IsVersion = true;

            return View(model.Layout, model);
        }

        public ActionResult EditPage(string versionId)
        {
            bool viewFlag = false;
            bool editFlag = true;

            Meanstream.Portal.Core.Content.PageVersion page = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(new Guid(versionId));

            if (page == null)
                return View();

            List<Meanstream.Portal.Core.Membership.Role> roles = null;
            Meanstream.Portal.Core.Membership.Role AllUsersRole = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS);

            if (Request.IsAuthenticated)
            {
                roles = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRolesForUser(HttpContext.Profile.UserName);

                foreach (Meanstream.Portal.Core.Membership.Role Role in roles)
                {
                    if (HasViewPagePermissionsVersion(page.Id, Role.Id))
                    {
                        viewFlag = true;
                    }

                    if (HasEditPagePermissionsVersion(page.Id, Role.Id) | Meanstream.Portal.Core.Membership.MembershipService.Current.IsUserInRole(HttpContext.Profile.UserName, Meanstream.Portal.Core.AppConstants.ADMINISTRATOR))
                    {
                        editFlag = true;
                    }
                }
            }
            else
            {
                viewFlag = false;
                editFlag = false;
            }

            if (viewFlag == false)
            {
                //go to error page
                FormsAuthentication.RedirectToLoginPage();
            }
            
            PortalPageViewModel model = new PortalPageViewModel();
            model.Id = page.PageId;
            model.PortalId = page.PortalId;
            model.VersionId = page.Id;
            model.Layout = page.Skin.Path;
            model.MetaDescription = page.MetaDescription;
            model.MetaKeywords = page.MetaKeywords;
            model.MetaTitle = page.MetaTitle;
            model.Author = page.Author;
            model.EndDate = page.EndDate;
            model.StartDate = page.StartDate;
            model.Url = page.Url;
            model.Name = page.Name;

            foreach (Meanstream.Portal.Core.Content.SkinZone zone in page.Skin.Zones)
            {
                //get widgets for zone and sort
                List<WidgetVersion> zoneWidgets = new List<WidgetVersion>();
                IEnumerable<WidgetVersion> widgets = page.Widgets.Where(w => w.SkinPaneId == zone.Id);
                widgets = from widget in widgets orderby widget.DisplayOrder select widget;

                foreach (WidgetVersion widget in widgets)
                {
                    viewFlag = false;
                    editFlag = false;

                    if (Request.IsAuthenticated)
                    {
                        foreach (Meanstream.Portal.Core.Membership.Role role in roles)
                        {
                            if (HasViewModulePermissionsVersion(widget.Id, role.Id))
                            {
                                viewFlag = true;
                            }

                            if (HasEditModulePermissionsVersion(widget.Id, role.Id) | Meanstream.Portal.Core.Membership.MembershipService.Current.IsUserInRole(HttpContext.Profile.UserName, Meanstream.Portal.Core.AppConstants.ADMINISTRATOR))
                            {
                                editFlag = true;
                            }
                        }
                    }
                    else
                    {
                        if (HasViewModulePermissionsVersion(widget.Id, AllUsersRole.Id))
                        {
                            viewFlag = true;
                        }
                    }

                    if (viewFlag)
                    {
                        //add to widget zone list
                        zoneWidgets.Add(widget);
                    }
                }

                model.WidgetVersions.Add(zone.Pane, zoneWidgets);
            }

            model.IsPreview = false;
            model.IsEditable = true;
            model.IsVersion = true;

            return View(model.Layout, model);
        }

        public ActionResult PreviewSkin(string skinId)
        {
            Meanstream.Portal.Core.Content.Skin skin = Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(new Guid(skinId));

            PortalPageViewModel model = new PortalPageViewModel();
            model.Id = new Guid();
            model.PortalId = new Guid();
            model.VersionId = new Guid();
            model.Layout = skin.Path;
            model.MetaDescription = "";
            model.MetaKeywords = "";
            model.MetaTitle = "";
            model.Author = "";
            model.Name = "";
            model.IsPreview = true;
            model.IsEditable = false;
            model.IsVersion = true;

            return View(skin.Path, model);
        }

        #region "publish page caching"
        private static Meanstream.Portal.Core.Entities.MeanstreamPage GetPageCache(string url)
        {
            Guid portalId = PortalContext.Current.PortalId;
            Meanstream.Portal.Core.Entities.MeanstreamPage page = null;
            
            if (page != null)
            {
                return page;
            }

            if (url == null || url == "index" || url == "default.aspx")
            {
                try
                {
                    return Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId=" + portalId.ToString() + " AND IsHome=True")[0];
                }
                catch (Exception ex)
                {
                    PortalTrace.Fail(String.Concat("GetPageCache() ", "Home page has not been assigned url=" + url + " Exception: " + ex.Message), DisplayMethodInfo.FullSignature);
                }
            }
            //handle returnURL issue (.net returns ',' with paramaters) 
            if (url.Contains(","))
            {
                url = url.Split(',')[0];
            }
            Meanstream.Portal.Core.Data.MeanstreamPageQuery Query = new Meanstream.Portal.Core.Data.MeanstreamPageQuery();
            Query.AppendEquals(Meanstream.Portal.Core.Entities.MeanstreamPageColumn.Url, url.Trim());
            Query.AppendEquals("AND", Meanstream.Portal.Core.Entities.MeanstreamPageColumn.Type, "1");
            Query.AppendEquals("AND", Meanstream.Portal.Core.Entities.MeanstreamPageColumn.PortalId, portalId.ToString());

            try
            {
                page = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find(Query.GetParameters())[0];
                if (page.EnableCaching == true && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
                {
                    PortalTrace.WriteLine("Caching.... " + url);
                    System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE + "_ENTITY_" + url + "_PORTAL_" + portalId.ToString(), page, null, DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["Meanstream.PageCacheExpiration"])), TimeSpan.Zero);
                }
                return page;
            }
            catch (Exception ex)
            {
                PortalTrace.Fail(String.Concat("GetPageCache() ", " PAGE NOT FOUND " + url), DisplayMethodInfo.DoNotDisplay);
            }
            return page;
        }

        private static Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.AspnetRoles> GetUserRoleCache(string Username, bool enableCaching)
        {
            Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.AspnetRoles> Roles = null;
            if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
            {
                Roles = (Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.AspnetRoles>)System.Web.HttpContext.Current.Cache[Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES + "_ENTITIES_" + Username];
            }
            if (Roles == null)
            {
                Guid UserID = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.Find("UserName=" + Username)[0].UserId;
                Roles = Meanstream.Portal.Core.Data.DataRepository.AspnetRolesProvider.GetByUserIdFromAspnetUsersInRoles(UserID);
                if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
                {
                    System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES + "_ENTITIES_" + Username, Roles, null, DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["Meanstream.PageCacheExpiration"])), TimeSpan.Zero);
                }
            }
            return Roles;
        }

        private static Meanstream.Portal.Core.Membership.Role GetAllUsersRole(bool enableCaching)
        {
            Meanstream.Portal.Core.Membership.Role Roles = null;
            if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
            {
                Roles = (Meanstream.Portal.Core.Membership.Role) System.Web.HttpContext.Current.Cache[Meanstream.Portal.Core.AppConstants.ALLUSERS];
            }
            if (Roles == null)
            {
                Roles = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS);
                if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
                {
                    System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.AppConstants.ALLUSERS, Roles, null, DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["Meanstream.PageCacheExpiration"])), TimeSpan.Zero);
                }
            }
            return Roles;
        }

        private static Meanstream.Portal.Core.Entities.MeanstreamSkins GetSkin(Guid skinId, bool enableCaching)
        {
            Guid portalId = PortalContext.Current.PortalId;
            Meanstream.Portal.Core.Entities.MeanstreamSkins skin = null;
            if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
            {
                skin = (Meanstream.Portal.Core.Entities.MeanstreamSkins) System.Web.HttpContext.Current.Cache[Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE + "_SKIN_ENTITY_" + skinId.ToString() + "_PORTAL_" + portalId.ToString()];
            }
            if (skin == null)
            {
                skin = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.GetById(skinId);
                if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
                {
                    System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE + "_SKIN_ENTITY_" + skinId.ToString() + "_PORTAL_" + portalId.ToString(), skin, null, DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["Meanstream.PageCacheExpiration"])), TimeSpan.Zero);
                }
            }
            return skin;
        }

        private static Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamSkinPane> GetZone(Guid skinId, bool enableCaching)
        {
            Guid portalId = PortalContext.Current.PortalId;
            Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamSkinPane> zones = null;
            if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
            {
                zones = (Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamSkinPane>)System.Web.HttpContext.Current.Cache[Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE + "_SKIN_ZONE_ENTITY_" + skinId.ToString() + "_PORTAL_" + portalId.ToString()];
            }
            if (zones == null)
            {
                zones = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Find("SkinId=" + skinId.ToString());
                if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
                {
                    System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE + "_SKIN_ZONE_ENTITY_" + skinId.ToString() + "_PORTAL_" + portalId.ToString(), zones, null, DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["Meanstream.PageCacheExpiration"])), TimeSpan.Zero);
                }
            }
            return zones;
        }

        private static Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamPagePermission> GetPagePermission(Guid pageId, bool enableCaching)
        {
            Guid portalId = PortalContext.Current.PortalId;
            Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamPagePermission> permissions = null;
            if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
            {
                permissions = (Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamPagePermission>)System.Web.HttpContext.Current.Cache[Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE + "_PAGE_PERMISSION_ENTITY_" + pageId.ToString() + "_PORTAL_" + portalId.ToString()];
            }
            if (permissions == null)
            {
                permissions = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Find("PageId=" + pageId.ToString());
                if (enableCaching && bool.Parse(ConfigurationManager.AppSettings["Meanstream.EnableCaching"]))
                {
                    System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE + "_PAGE_PERMISSION_ENTITY_" + pageId.ToString() + "_PORTAL_" + portalId.ToString(), permissions, null, DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["Meanstream.PageCacheExpiration"])), TimeSpan.Zero);
                }
            }
            return permissions;
        }

        private static Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamModule> GetModules(Guid pageId)
        {
            Meanstream.Portal.Core.Data.MeanstreamModuleQuery Query = new Meanstream.Portal.Core.Data.MeanstreamModuleQuery();
            Query.AppendEquals(Meanstream.Portal.Core.Entities.MeanstreamModuleColumn.PageId, pageId.ToString());
            Query.AppendLessThanOrEqual("AND", Meanstream.Portal.Core.Entities.MeanstreamModuleColumn.StartDate, System.DateTime.Now.ToString());
            Query.AppendGreaterThanOrEqual("AND", Meanstream.Portal.Core.Entities.MeanstreamModuleColumn.EndDate, System.DateTime.Now.ToString());
            Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamModule> Modules = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Find(Query.Parameters);
            Modules.Sort("DisplayOrder");
            return Modules;
        }

        private static Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamModulePermission> GetWidgetPermissions(Guid widgetId)
        {
            return Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Find("ModuleId=" + widgetId.ToString());
        }

        private static bool HasViewModulePermissions(Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamModulePermission> ModulePermissions, Guid ModuleId, Guid RoleId)
        {
            ModulePermissions = ModulePermissions.FindAll("ModuleId", ModuleId);
            if (ModulePermissions == null)
            {
                return false;
            }
            ModulePermissions = ModulePermissions.FindAll("PermissionId", Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id);
            if (ModulePermissions == null)
            {
                return false;
            }
            Meanstream.Portal.Core.Entities.MeanstreamModulePermission ModulePermission = ModulePermissions.Find("RoleId", RoleId);
            if (ModulePermission == null)
            {
                return false;
            }
            return true;
        }

        private static bool HasViewPagePermissions(Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.MeanstreamPagePermission> PagePermissions, Guid RoleId)
        {
            PagePermissions = PagePermissions.FindAll("PermissionId", Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id);
            if (PagePermissions.Count == 0)
            {
                return false;
            }
            Meanstream.Portal.Core.Entities.MeanstreamPagePermission PagePermission = PagePermissions.Find("RoleId", RoleId);
            if (PagePermission == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region "version page"
        private static bool HasEditPagePermissionsVersion(Guid VersionID, Guid RoleId)
        {
            if (Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Find("VersionId=" + VersionID.ToString() + " AND RoleId=" + RoleId.ToString() + " AND PermissionId=" + Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id.ToString()).Count == 0)
            {
                return false;
            }
            return true;
        }

        private static bool HasViewPagePermissionsVersion(Guid VersionID, Guid RoleId)
        {
            if (Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Find("VersionId=" + VersionID.ToString() + " AND RoleId=" + RoleId.ToString() + " AND PermissionId=" + Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id.ToString()).Count == 0)
            {
                return false;
            }
            return true;
        }

        private static bool HasEditModulePermissionsVersion(Guid ModuleID, Guid ID)
        {
            if (Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("RoleId=" + ID.ToString() + " AND PermissionId=" + Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id.ToString() + " AND ModuleId=" + ModuleID.ToString()).Count == 0)
            {
                return false;
            }
            return true;
        }

        private static bool HasViewModulePermissionsVersion(Guid ModuleID, Guid RoleId)
        {
            if (Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("RoleId=" + RoleId.ToString() + " AND PermissionId=" + Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id.ToString() + " AND ModuleId=" + ModuleID.ToString()).Count == 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}