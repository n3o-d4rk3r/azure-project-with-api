using Kitchen.Data.Enumerators;

namespace Kitchen.Data.Models
{
    public class User : BaseClass
    {
        public string uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Status Status { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
