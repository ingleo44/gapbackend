using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace medAppointments.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {


        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }


        /// <summary>
        /// Create authentication key to user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse), 200)]
        [ProducesResponseType(typeof(ServiceResponse), 401)]
        [ProducesResponseType(typeof(ServiceResponse), 500)]
        public async Task<IActionResult> Login(AuthCredentials credentials)
        {
            var result = await _authenticationServices.Login(credentials.userName, credentials.password);
            return result.success ? new ObjectResult(result) : Unauthorized(result);
        }
    }



    public class AuthCredentials
    {
        public string userName { get; set; }

        public string password { get; set; }
    }
}