using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CitiesLocation.Models
{
    public static class SeedData
    {
        
        public static void Initialize(DBInteractor db)
        {
            db.Database.EnsureCreated();
            
            if (!db.Cities.Any())
            {
                var cities = City.GetCitiesFromCsv();
                foreach (var city in cities)
                {
                    db.Cities.Add(city);
                }
                db.SaveChanges();
            }
        }
    }
}
