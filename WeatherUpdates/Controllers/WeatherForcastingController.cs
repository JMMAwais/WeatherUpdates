using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WeatherUpdates.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherUpdates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForcastingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public WeatherForcastingController(IConfiguration configuration)
        {
                _configuration = configuration;
        }

        [HttpGet("GetMyWeather")]
        public async Task<IActionResult> Get(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.openweathermap.org");
                    var response =await client.GetAsync($"/data/2.5/weather?q={city}&appid=e1e9a5789ea2786880ca9549d0d52d06");
                    response.EnsureSuccessStatusCode();

                    var stringResult =await  response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
   
                    var Celcius = rawWeather.Main.Temp;
                    int Fahrenhiet=(int)((Celcius - 273.15) * 9 / 5 + 32);
                    

                    return Ok(new
                    {

                        Celcius = rawWeather.Main.Temp+"C",
                        Farenhiet = Fahrenhiet+"F",
                        feels_like = rawWeather.Main.feels_like,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        WeatherCondition = string.Join(",", rawWeather.Weather.Select(x => x.Description)),
                        City = rawWeather.Name,
                        Location = rawWeather.Sys.country + " " + rawWeather.Name


                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        // GET api/<WeatherForcastingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WeatherForcastingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WeatherForcastingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeatherForcastingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
