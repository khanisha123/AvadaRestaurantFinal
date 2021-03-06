using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class HomeVM
    {
        public fineDiningExperience fineDiningExperience { get; set; }
        public List<Bulletin> bulletins { get; set; }
        public List<HorsDoeuvresProduct> HorsDoeuvresProduct { get; set; }
        public List<MainCourseProducts> MainCourseProducts { get; set; }
        public List<DessertCoffeeProducts> DessertCoffeeProducts { get; set; }
        public foodIsOurCommon foodIsOurCommon { get; set; }
        public TheBestTableInTown TheBestTableInTown { get; set; }
        public NewYorkTimes NewYorkTimes { get; set; }
        public KungPao kungPao { get; set; }
        public List<GuardianGlobe> guardianGlobes { get; set; }
        public TakeoutNowAvailable takeoutNowAvailables { get; set; }
        public BraisedAbalone braisedAbalone { get; set; }
        public List<Product> products { get; set; }

    }
}
