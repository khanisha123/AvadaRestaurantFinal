using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.DAL
{
    public class Context:IdentityDbContext<AppUser>
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
        public DbSet<TeamHeader> TeamHeader { get; set; }
        public DbSet<MembersTeam> MembersTeam { get; set; }
        public DbSet<HorsDoeuvresProduct> HorsDoeuvresProduct { get; set; }
        public DbSet<MainCourseProducts> MainCourseProducts { get; set; }
        public DbSet<DessertCoffeeProducts> DessertCoffeeProducts { get; set; }
        public DbSet<DrinksCocktailsProducts> DrinksCocktailsProducts { get; set; }
        public DbSet<Bulletin> Bulletin { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesProduct> salesProducts { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<Table> Table { get; set; }
    }
}
