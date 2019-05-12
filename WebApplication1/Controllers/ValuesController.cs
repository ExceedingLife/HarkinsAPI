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

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
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
                return Ok("User POST wasn't successful...");
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
