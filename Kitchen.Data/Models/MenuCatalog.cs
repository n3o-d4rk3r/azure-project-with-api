using Kitchen.Data.Enumerators;
using System.Collections.Generic;

namespace Kitchen.Data.Models
{
    public class MenuCatalog : BaseClass
    {
        public string MenuCatalogID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotographUrl { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public Category Category { get; set; }

    }
}
