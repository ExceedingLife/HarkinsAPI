using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Controllers;
using WebApplication1.Controllers.Data_Access;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        private DataAccessUser da = new DataAccessUser();

        // GET api/values/GetUsers
        //public IEnumerable<string> Get()IEnumerable<User>
        //[Route("users/all")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            try
            {
                var dbUsers = da.GetAllUsers();
                var lstUsers = new List<User>();

                foreach(var user in dbUsers)
                {
                    lstUsers.Add(new User
                    {
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        DateCreated = user.DateCreated,
                        Email = user.Email,
                        UserName = user.UserName,
                        Password = user.Password
                    });
                }
                return Ok(lstUsers);
                //return Ok("Retrieved ALL Users");
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                //return null;
                throw;
                //return BadRequest("Error Getting all Users");
            }
            
        }

        // GET api/values/Get?id=3
        //public string Get(int id)
        [Route("api/values/Get/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var user = da.GetUserById(id);
                if(user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        // POST api/values/PostUser
        // public void Post([FromBody]string value) { }
        [HttpPost]
       public IHttpActionResult PostUser([FromBody]User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                da.CreateUser(user);
                return Ok("User CREATED Successfully!!");
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return BadRequest("User POST wasn't successful...");
            }
        }

        // PUT api/values/PutUser?id=6
        [HttpPut]
        public IHttpActionResult PutUser(int id, [FromBody]User value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                da.UpdateUser(value);
                return Ok(value);
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        // DELETE api/values/Delete?id=5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                da.DeleteUser(id);
                return Ok("User Deleted Successfully");
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
    }
}
