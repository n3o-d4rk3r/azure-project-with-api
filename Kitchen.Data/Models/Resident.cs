namespace Kitchen.Data.Models
{
    public class Resident : BaseClass
    {
        public string ResidentID { get; set; }
        public string RoomNumber { get; set; }
        public string FluidLevel { get; set; }
        public string DietLevel { get; set; }
        public string Allergies { get; set; }
        public string GroupID { get; set; }
    }
}
