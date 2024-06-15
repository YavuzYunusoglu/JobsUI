using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MesleklerUI
{
    public class Configuration : IRocketPluginConfiguration
    {
        public List<Job> jobs;
        public void LoadDefaults()
        {
            jobs = new List<Job>()
            {
                new Job()
                {
                    jobName = "Uber",
                    jobId = "uber",
                    jobImage = "https://media.discordapp.net/attachments/938154666603188234/1243877998529347584/UberBlack.png"
                },
                new Job()
                {
                    jobName = "Police",
                    jobId = "police",
                    jobImage = "https://static.vecteezy.com/system/resources/previews/028/585/656/non_2x/police-face-3d-rendering-icon-illustration-free-png.png"
                },
                new Job()
                {
                    jobName = "Doctor",
                    jobId = "doctor",
                    jobImage = "https://cdn3d.iconscout.com/3d/premium/thumb/doctor-5463469-4551646.png"
                },
                new Job()
                {
                    jobName = "Taxi",
                    jobId = "taxi",
                    jobImage = "https://cdn3d.iconscout.com/3d/premium/thumb/taxi-3485080-2914598.png"
                },
                new Job()
                {
                    jobName = "Food",
                    jobId = "food",
                    jobImage = "https://cdn3d.iconscout.com/3d/premium/thumb/chef-avatar-6299542-5187874.png"
                },
                new Job()
                {
                    jobName = "Car dealer",
                    jobId = "CarDealer",
                    jobImage = "https://static.vecteezy.com/system/resources/previews/024/253/425/original/car-for-sale-3d-illustration-png.png"
                },
            };
        }
    }
}
