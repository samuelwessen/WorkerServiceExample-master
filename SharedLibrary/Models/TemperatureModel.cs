using System;

namespace SharedLibrary.Models
{
    public class TemperatureModel
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Timestamp { get; set; }



        public TemperatureModel()
        {

        }

        public TemperatureModel(double temperature, double humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
            Timestamp = DateTime.Now;
        }
    }
}
