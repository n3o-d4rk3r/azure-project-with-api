using Kitchen.Data.Enumerators;

namespace Kitchen.Data.Models
{
    public class MenuItem : BaseClass
    {
        public string MenuItemID { get; set; }
        public string DailyMenuID { get; set; }
        public string MenuCatalogID { get; set; }
        public string? ItemName { get; set; }
        public string? PhotoUrl { get; set; }
        public Category? Category { get; set; }

    }
}
