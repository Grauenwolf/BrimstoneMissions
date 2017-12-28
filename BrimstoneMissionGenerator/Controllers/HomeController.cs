using BrimstoneMissionGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tortuga.Anchor;

namespace BrimstoneMissionGenerator.Controllers
{
    public class HomeController : Controller
    {
        private const string MissionsCookieName = "missions";

        public ActionResult Index()
        {
            var sets = FindSets();

            return View(sets);
        }

        private List<CheckList> FindSets()
        {
            var sets = Application.Missions.Set.Select(x => new CheckList() { MissionsSet = x }).ToList();

            if (Request.Cookies[MissionsCookieName] != null && !string.IsNullOrEmpty(Request.Cookies[MissionsCookieName].Value))
            {
                var list = Request.Cookies[MissionsCookieName].Value.Split(',').Select(x => int.Parse(x)).ToList();
                foreach (var set in sets)
                {
                    if (list.Contains(set.MissionsSet.Id))
                        set.Available = true;
                }
            }

            if (!sets.Any(x => x.Available))
                sets[0].Available = true;
            return sets;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        [HttpPost]
        public ActionResult Mission(FormCollection formData)
        {
            var selectedMissions = formData.AllKeys.Where(x => formData[x].Contains("true")).Select(x => x.Substring("Mission_".Length)).ToList();
            var userCookie = new HttpCookie(MissionsCookieName, string.Join(",", selectedMissions));
            userCookie.Expires = DateTime.Now.AddYears(1);
            Response.SetCookie(userCookie);

            return Redirect("/Home/Mission");
        }

        [HttpGet]
        public ActionResult Mission()
        {
            var sets = FindSets().Where(x => x.Available == true).ToList();
            var missions = new List<MissionPicker>();
            foreach (var set in sets)
                foreach (var mission in set.MissionsSet.Mission)
                    missions.Add(new MissionPicker() { MissionsSet = set.MissionsSet, Mission = mission });

            var dice = new RandomExtended();
            var selectedMission = dice.Choose(missions);


            return View(selectedMission);
        }
        [HttpGet]
        public ActionResult AllMissions()
        {
            return View(Application.Missions.Set);
        }

    }
}