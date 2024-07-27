namespace WeatherUpdates.Models
{
    public class OpenWeatherResponse
    {
        public string Name { get; set; }
        public decimal TempFahrenheit {  get; set; }  

        public IEnumerable<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }

        public SYS Sys { get; set; }

       
    }
}
