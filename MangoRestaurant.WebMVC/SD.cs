namespace MangoRestaurant.WebMVC
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }

        public enum ApiMethod
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }
}
