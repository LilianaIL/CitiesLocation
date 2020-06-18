using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CitiesLocation.Models
{
    [Table("cities")]
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public string AsciiName { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string Country { get; set; }
        public string Iso2 { get; set; }
        public string Iso3 { get; set; }
        public string AdminName { get; set; }
        public string Capital { get; set; }
        public Nullable<double> Population { get; set; }


        public static City FromCsv(string csvLine)
        {
            string[] values = csvLine.Replace("\",", ";").Replace("\"", "").Split(';');

            City city = new City();
            var intRes = IntValues(values[10]);
            city.CityId = intRes.Item2;
            city.Name = values[0];
            city.AsciiName = values[1];

            var doubleRes = DoubleValues(values[2]);
            city.Latitude = doubleRes.Item1 ? (double?)doubleRes.Item2 : null;

            doubleRes = DoubleValues(values[3]);
            city.Longitude = doubleRes.Item1 ? (double?)doubleRes.Item2 : null;
            city.Country = values[4];
            city.Iso2 = values[5];
            city.Iso3 = values[6];
            city.AdminName = values[7];
            city.Capital = values[8];

            doubleRes = DoubleValues(values[9]);
            city.Population = doubleRes.Item1 ? (double?)doubleRes.Item2 : null;
            return city;
        }

        private static (bool, double) DoubleValues(string value)
        {
            double d;
            var res = double.TryParse(value, out d);
            return (res, d);
        }

        private static (bool, int) IntValues(string value)
        {
            int d;
            var res = int.TryParse(value, out d);
            return (res, d);
        }

        public static List<City> GetCitiesFromCsv()
        {
            return File.ReadAllLines("worldcities.csv")
                                           .Skip(1)
                                           .Select(v => City.FromCsv(v))
                                           .ToList();
        }
    }


    public class LocationForm
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string UserId { get; set; }
        public double Radius { get; set; }
    }
}
