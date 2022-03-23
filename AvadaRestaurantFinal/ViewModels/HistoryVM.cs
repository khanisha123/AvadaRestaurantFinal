using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class HistoryVM
    {
        public HistoryHeader historyHeader  { get; set; }
        public PullUpAChair PullUpAChair { get; set; }
        public fineDiningExperience fineDiningExperience { get; set; }
        public TakeoutNowAvailableHistory takeoutNowAvailableHistory { get; set; }
        public KungPaoChickenHistory KungPaoChickenHistory { get; set; }
        public List<MeetTheTeam> MeetTheTeam { get; set; }
    }
}
