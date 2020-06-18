using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CitiesLocation.Models;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;

namespace CitiesLocation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        DBInteractor db;
        
        public UsersController(DBInteractor context)
        {
            db = context;            
        }


        [HttpPost]        
        public async Task<ActionResult<User>> Login(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            User userInDb = await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (userInDb != null)
            {
                userInDb.GetUniqueToken();
                
                return userInDb;
            }

            return Ok(user);
        }



        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            user.UserId = "";
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

      
       
    }
}