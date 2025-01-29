using ApiRest.Helpers;
using Aplication.Interfaces;
using Aplication.Services;
using Domain.Common;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{

    /// <summary>
    /// Controller of Property Trace
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IPropertyTraceService propertyTraceService;


        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyTraceController(IPropertyTraceService propertyTraceService)
        {
            this.propertyTraceService = propertyTraceService;
        }


        /// <summary>
        /// Method of property Trace  update       
        /// </summary>
        ///  
        /// <returns></returns>
        /// /// <remarks>
        /// Request example:
        ///     
        ///     {
        ///         "IdPropertyTrace": 1,
        ///         "IdProperty": 1,
        ///         "DateSale": "2025-01-29",
        ///         "NameTrace": "dfsdfsdfsdfdsfsd",
        ///         "Value": 1200,
        ///         "Tax": 1.2  
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] PropertyTraceUpdateRequest request)
        {
            try
            {
                var result = await propertyTraceService.Update(request);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}
