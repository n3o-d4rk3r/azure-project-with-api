namespace Kitchen.Data.Models
{
    public class MealRequest : BaseClass
    {
        public string? MealRequestID { get; set; }
        public string Resident { get; set; }
        public string? DailyMenuID { get; set; }

        public string? BreakfastName { get; set; }
        public string? BreakfastPhotoUrl { get; set; }

        public string? TeaRoundName { get; set; }
        public string? TeaRoundPhotoUrl { get; set; }

        public string? DinnerName { get; set; }
        public string? DinnerPhotoUrl { get; set; }

        public string? SmallSnackName { get; set; }
        public string? SmallSnackPhotoUrl { get; set; }

        public string? EveningMealName { get; set; }
        public string? EveningMealPhotoUrl { get; set; }
    }
}
