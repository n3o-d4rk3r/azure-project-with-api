using System;

namespace Kitchen.Data.Models
{
    public class BaseClass
    {
        public DateTime DateTime { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? ClientID { get; set; }
    }
}
