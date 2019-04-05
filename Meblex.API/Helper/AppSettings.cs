namespace Meblex.API.Helper
{
    public class AppSettings
    {
        public AppSettings()
        {
            Secret = System.Environment.GetEnvironmentVariable("JWT_SECRET");
        }
        public string Secret { get; set; }

        public int ExpiredAfterDays { get; set; }
    }
}
