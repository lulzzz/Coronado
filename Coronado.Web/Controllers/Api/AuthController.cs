using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coronado.Web.Data;
using Coronado.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coronado.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        [Route("[action]")]
        public ActionResult Google([FromBody] GoogleAuthenticationDto dto)
        {

            return Ok();
        }
    }

    public class GoogleAuthenticationDto {
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}