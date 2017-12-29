using BrimstoneMissionGenerator.Models;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BrimstoneMissionGenerator
{
    public class Application : System.Web.HttpApplication
    {
        public static Missions Missions;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var file = new FileInfo(Path.Combine(Server.MapPath("~/App_Data"), "Missions.xml"));
            var settingXmlSerializer = new XmlSerializer(typeof(Missions));
            using (var stream = file.OpenRead())
                Missions = (Missions)settingXmlSerializer.Deserialize(stream);

            var products = XElement.Load(Path.Combine(Server.MapPath("~/App_Data"), "Products.xml"));
            foreach (var node in products.Elements())
            {
                var setting = Missions.Set.SingleOrDefault(x => x.Id.ToString() == node.Attribute("Id").Value);
                if (setting != null)
                {
                    setting.ProductHtml = new MvcHtmlString(node.Value);
                }
            }

        }
    }
}
