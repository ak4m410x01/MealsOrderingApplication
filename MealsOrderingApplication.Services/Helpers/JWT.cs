namespace MealsOrderingApplication.Services.Helpers
{
    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInDays { get; set; }
        public string Key { get; set; }
    }
}
