using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CitiesLocation.Models;
using System.Threading.Tasks;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitiesLocation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CitiesController : ControllerBase
    {
        DBInteractor db;

        public CitiesController(DBInteractor context)
        {
            db = context;            
        }

        

        // POST api/cities
        [HttpPost]
        public async Task<ActionResult<IEnumerable<City>>> CitiesList(LocationForm form)
        {
            if (form == null)
            {
                return BadRequest();
            }
            
            var token = Request.Cookies["token"];
            
            if (token != null && CheckToken(token)) 
            {
                var degree = form.Radius / 111;

                var cities = await db.Cities.Where(c => c.Latitude >= (form.Latitude - degree) && c.Latitude <= (form.Latitude + degree) &&
                                                        c.Longitude >= (form.Longitude - degree) && c.Longitude <= (form.Longitude + degree))
                                            .OrderBy(c=>c.Longitude).ToListAsync();
                return Ok(new { status = true, result = cities });
            }

            
            return Ok(new { status = false, result = new { } });
        }

        
        private bool CheckToken(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-10))
            {
                return false;
            }
            return true;
        }
    }
}
