using AvadaRestaurantFinal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.DAL
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<fineDiningExperience> FineDiningExperience { get; set; }
        public DbSet<foodIsOurCommon> foodIsOurCommons { get; set; }
        public DbSet<TheBestTableInTown> theBestTableInTowns { get; set; }
        public DbSet<NewYorkTimes> newYorkTimes { get; set; }
        public DbSet<GuardianGlobe> GuardianGlobe { get; set; }
        public DbSet<KungPao> kungPao { get; set; }
        public DbSet<TakeoutNowAvailable> takeoutNowAvailable { get; set; }
        public DbSet<BraisedAbalone> braisedAbalone { get; set; }
        public DbSet<HistoryHeader> historyHeader { get; set; }
        public DbSet<PullUpAChair> PullUpAChair { get; set; }
        public DbSet<TakeoutNowAvailableHistory> TakeoutNowAvailableHistory { get; set; }
        public DbSet<KungPaoChickenHistory> KungPaoChickenHistory { get; set; }
        public DbSet<MeetTheTeam> MeetTheTeam { get; set; }
        public DbSet<ReceptionBarHeader> ReceptionBarHeader { get; set; }
        public DbSet<FineDiningProfesionnal> FineDiningProfesionnal { get; set; }
        public DbSet<PlanYourReception> PlanYourReception { get; set; }
        public DbSet<BestCocktailsInTown> bestCocktailsInTown { get; set; }
        public DbSet<PerfectPlaceForAReception> PerfectPlaceForAReception { get; set; }
        public DbSet<ReceptionBarEndSection> ReceptionBarEndSection { get; set; }
    }
}
