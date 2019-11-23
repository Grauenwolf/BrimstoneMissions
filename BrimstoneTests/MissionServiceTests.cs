using BrimstoneMissionGenerator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BrimstoneTests
{
    [TestClass]
    public class MissionServiceTests
    {
        [TestMethod]
        public void DuplicateMissionCheck()
        {
            var service = new MissionService("");

            foreach (var set in service.Sets)
            {
                var missionNumbers = new HashSet<int>(set.Missions.Count);

                foreach (var mission in set.Missions)
                    Assert.IsTrue(missionNumbers.Add(mission.Number), $"Mission number {mission.Number} in set {set.Id}/{set.Name} appears twice");
            }
        }
    }
}
