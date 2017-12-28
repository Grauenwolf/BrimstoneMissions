using BrimstoneMissionGenerator.Models;
using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
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

        }
    }
}
