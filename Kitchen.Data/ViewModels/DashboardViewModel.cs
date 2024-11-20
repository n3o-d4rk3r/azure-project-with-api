using Kitchen.Data.Models;
using System.Collections.Generic;

namespace Kitchen.Data.ViewModels
{
    public class DashboardViewModel
    {

        public int ResidentsCount { get; set; }
        public int MenuCount { get; set; }
        public List<Resident> RecentResidents { get; set; }
        public List<DailyMenu> TodaysMenu { get; set; }

    }
}
