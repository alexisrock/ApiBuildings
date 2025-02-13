using ApiRest.Helpers;
using Application.Interfaces;
using Application.Services;
using Domain.Common;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    /// <summary>
    /// Controller of Property
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {

        private readonly IPropertyService propertyService;

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }


        /// <summary>
        /// Method of list all owner       
        /// </summary>
        ///  
        /// <returns></returns>

        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string? CodeInternal, int? Year, string? Name, int? IdOwner, int Pagina, int TamanioPagina)
        {

            var resukt = await propertyService.GetPropertiesAll(new FiltroProperty()
            {
                TamanioPagina = TamanioPagina,
                Pagina = Pagina,
                CodeInternal = CodeInternal,
                Year = Year,
                Name = Name,
                IdOwner = IdOwner
            });
            return Ok(resukt);
        }




        /// <summary>
        /// Method of property create       
        /// </summary>
        ///  
        /// <returns></returns>
        /// /// <remarks>
        /// Request example:
        ///     
        ///     {
        ///             "Name": "casa",
        ///             "Address": "calle 13 # 56 - 24",
        ///             "Price": 1000.34,
        ///             "CodeInternal": "C757567",
        ///             "Year": 20,
        ///             "IdOwner": 1,
        ///             "FileImage": "C://imagen",
        ///             "Enabled": true,
        ///             "DateSale": "2025-01-29",
        ///             "NameTrace": "dfsdfsdfsdfdsfsd",
        ///             "Value": 1200,
        ///             "Tax": 1.2        
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PropertyRequest request)
        {
            var result = await propertyService.Create(request);
            return Ok(result);

        }


        /// <summary>
        /// Method of property update       
        /// </summary>
        ///  
        /// <returns></returns>
        /// /// <remarks>
        /// Request example:
        ///     
        ///     {
        ///         "IdProperty": 1,
        ///         "Name": "casa",
        ///         "Address": "calle 13 # 56 - 24",
        ///         "Price": 1000.34,
        ///         "CodeInternal": "C757567",
        ///         "Year": 20   ,
        ///         "IdOwner":1  
        ///     }
        ///
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] PropertyUpdateRequest request)
        {
            var result = await propertyService.Update(request);
            return Ok(result);

        }
    }
}
