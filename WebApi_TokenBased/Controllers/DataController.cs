using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebApi_TokenBased.Controllers
{
    public class DataController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/getanybody")]
        public IHttpActionResult AnybodyGet()
        {
            return Ok("The server time is: " + DateTime.Now.ToString() );
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/getforauthenticated")]
        public IHttpActionResult GetForAuthenticated()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }
        [Authorize(Roles="admin")]
        [HttpGet]
        [Route("api/data/getforadmin")]
        public IHttpActionResult GetForAdminAuthenticated()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            return Ok("Hello" + identity.Name + string.Join(",",roles.ToList()));
        }
    }
}
