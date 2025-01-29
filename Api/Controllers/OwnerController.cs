﻿using ApiRest.Helpers;
using Aplication.Interfaces;
using Aplication.Services;
using Domain.Common;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    /// <summary>
    /// Controller of Owner
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OwnerController : ControllerBase
    {



        private readonly IOwnerService _ownerService;


        /// <summary>
        /// Constructor
        /// </summary>
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listproducto = await _ownerService.GetAll();
                return Ok(listproducto);
            }
            catch (Exception)
            {
                return Problem();
            }
        }





        /// <summary>
        /// Method of owner create       
        /// </summary>
        ///  
        /// <returns></returns>
        /// /// <remarks>
        /// Request example:
        ///  
        ///     {
        ///        "Name": "Apartamento",
        ///        "Address": "calle 13 # 56 - 24",
        ///        "Photo": "C://imagen.foto",
        ///        "Birthday": "2025-01-23"
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] OwnerRequest pruebaSeleccionRequest)
        {
            try
            {
                var result = await _ownerService.Create(pruebaSeleccionRequest);
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
