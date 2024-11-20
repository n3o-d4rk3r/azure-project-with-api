using System;

namespace Kitchen.Data.Models
{
    public class Client : BaseClass
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Domain { get; set; }
        public int? ResidentLimit { get; set; }
        public DateTime? Date { get; set; }

    }
}
