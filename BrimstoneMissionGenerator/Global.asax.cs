using BrimstoneMissionGenerator.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ReadOnlyCollection<Product> Sets;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var file = new FileInfo(Path.Combine(Server.MapPath("~/App_Data"), "Missions.xml"));
            var settingXmlSerializer = new XmlSerializer(typeof(Models.Xml.Missions));

            Models.Xml.Missions sets;
            using (var stream = file.OpenRead())
                sets = (Models.Xml.Missions)settingXmlSerializer.Deserialize(stream);

            var products = XElement.Load(Path.Combine(Server.MapPath("~/App_Data"), "Products.xml"));

            var realSets = new List<Product>();
            foreach (var item in sets.Set)
            {
                var product = products.Elements().SingleOrDefault(x => x.Attribute("Id").Value == item.Id.ToString());
                MvcHtmlString productHtml = null;
                if (product != null)
                {
                    productHtml = new MvcHtmlString(product.Value);
                }
                realSets.Add(new Product(item.Id, item.Name, item.Mission.Select(x => new Mission(x.Name, x.Number, x.Page, x.Location, x.RandomWorlds, x.Notes, x.Intro)).ToList(), item.BggUrl, item.DownloadUrl, item.OtherWorld, productHtml));
            }

            Sets = new ReadOnlyCollection<Product>(realSets);

        }
    }
}
