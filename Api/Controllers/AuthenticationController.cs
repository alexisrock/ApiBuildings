using Domain.Dto;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Aplication.Interfaces;

namespace ApiRest.Controllers
{
    /// <summary>
    /// Controller of Authentication
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

       
        private readonly IUserService userService;

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticationController(  IUserService userService)
        {             
            this.userService = userService;
        }



        /// <summary>
        /// Method of authentication       
        /// </summary>
        ///  
        /// <returns></returns>
        /// /// <remarks>
        /// Request example:
        ///  
        ///     {
        ///        "UserName": "prueba",
        ///        "Password": "cHJ1RUJB"
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(UserTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authentication([FromBody] UserTokenRequest userTokenRequest)
        {
            try
            {
                var user = await userService.GetAuthentication(userTokenRequest);
                if (user.StatusCode == HttpStatusCode.OK)
                    return Ok(user);
                else
                    return Problem(user.Message, statusCode:(int)user.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }

        }






        /// <summary>
        /// Method of user create       
        /// </summary>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "UserName": "prueba",
        ///        "Password": "cHJ1RUJB"
        ///       
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserRequest userRequest)
        {
            try
            {
                var user = await userService.CreateUser(userRequest);
                if (user.StatusCode == HttpStatusCode.OK)
                    return Ok(user);
                else
                    return Problem(user.Message, statusCode: (int)user.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }

        }


    }
}
