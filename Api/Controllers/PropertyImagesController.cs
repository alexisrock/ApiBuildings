using ApiRest.Helpers;
using Aplication.Interfaces;
using Aplication.Services;
using Domain.Common;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{

    /// <summary>
    /// Controller of Property Images
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyImagesController : ControllerBase
    {

        private readonly IPropertyImageService propertyImageService;



        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyImagesController(IPropertyImageService propertyImageService)
        {
            this.propertyImageService = propertyImageService;
        }

        /// <summary>
        /// Method of property Images  update       
        /// </summary>
        ///  
        /// <returns></returns>
        /// /// <remarks>
        /// Request example:
        ///     
        ///     {
        ///         "IdPropertyImage": 1,
        ///         "IdProperty": 1,
        ///         "FileImage": "C://imagen",
        ///         "Enabled": true      
        ///     }
        ///
        /// </remarks>



        [HttpPut, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] PropertyImagesUpdateRequest request)
        {
            try
            {
                var result = await propertyImageService.Update(request);
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
