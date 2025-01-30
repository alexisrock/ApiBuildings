using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiRest.Controllers;
using Aplication.Interfaces;
using Domain.Common;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiBuildTest.Api
{
    [TestFixture]
    public class PropertyControllerTest
    {

        private Mock<IPropertyService> Mock;
        private PropertyController _controller;

        [SetUp]
        public void Setup()
        {
            Mock = new Mock<IPropertyService>();
            _controller = new PropertyController(Mock.Object);
        }


        [Test]
        public async Task GetAll_ReturnsOkResult_WithProperties()
        {
            // Arrange
            var filter = new FiltroProperty // Create a FiltroProperty object
            {
                TamanioPagina = 10,
                Pagina = 1,
                CodeInternal = "TestCode",
                Year = 2024,
                Name = "TestName",
                IdOwner = 1
            };
            var properties = new List<PropertyResponse> { new PropertyResponse(), new PropertyResponse() }; // Example properties
            Mock.Setup(x => x.GetPropertiesAll(It.IsAny<FiltroProperty>())).ReturnsAsync(properties); // Use It.IsAny

            // Act
            var result = await _controller.GetAll(filter.CodeInternal, filter.Year, filter.Name, filter.IdOwner, filter.Pagina, filter.TamanioPagina);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(properties));
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }


        [Test]
        public async Task GetAll_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            Mock.Setup(x => x.GetPropertiesAll(It.IsAny<FiltroProperty>())).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetAll(null, null, null, null, 1, 10);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var problemResult = (ObjectResult)result;
            Assert.That(problemResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));

            var problemDetails = problemResult.Value as ProblemDetails;
            Assert.IsNotNull(problemDetails);
            Assert.That(problemDetails.Detail, Is.EqualTo(exceptionMessage));
        }


        [Test]
        public async Task Create_ReturnsOk_WhenPropertyCreated()
        {
            // Arrange
            var request = new PropertyRequest { /* Initialize with valid data */ }; // Important!
            var baseResponse = new BaseResponse { StatusCode = HttpStatusCode.OK, /* ... other properties */ };
            Mock.Setup(x => x.Create(request)).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Create(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(okResult.Value, Is.EqualTo(baseResponse)); // Check the returned BaseResponse
        }

        [Test]
        public async Task Create_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {
            // Arrange
            var request = new PropertyRequest { /* Initialize with valid data */ }; // Important!
            var baseResponse = new BaseResponse { StatusCode = HttpStatusCode.BadRequest, /* ... other properties */ };
            Mock.Setup(x => x.Create(request)).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Create(request);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            Assert.That(((BadRequestResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));

        }

        [Test]
        public async Task Create_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            var request = new PropertyRequest { /* Initialize with valid data */ }; // Important!
            var exceptionMessage = "Test exception";
            Mock.Setup(x => x.Create(request)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Create(request);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result); // Or ProblemResult if you return that directly
            var problemResult = (ObjectResult)result;
            Assert.That(problemResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));

            var problemDetails = problemResult.Value as ProblemDetails;
            Assert.IsNotNull(problemDetails);

        }


    }

}

