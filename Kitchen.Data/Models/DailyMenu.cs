using System;
using System.Collections.Generic;

namespace Kitchen.Data.Models
{
    public class DailyMenu : BaseClass
    {
        public string DailyMenuID { get; set; }
        public DateTime Date { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public string? Day { get; set; }



    }
}
