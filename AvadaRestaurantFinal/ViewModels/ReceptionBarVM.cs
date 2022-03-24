using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class ReceptionBarVM
    {
       public ReceptionBarHeader receptionBarHeader { get; set; }
       public List<FineDiningProfesionnal> FineDiningProfesionnal { get; set; }
       public PlanYourReception planYourReception { get; set; }
       public BestCocktailsInTown bestCocktailsInTown { get; set; }
       public PerfectPlaceForAReception perfectPlaceForAReception { get; set; }
       public ReceptionBarEndSection ReceptionBarEndSection { get; set; }
    }
}
