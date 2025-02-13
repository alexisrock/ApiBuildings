using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiRest.Controllers;
using Application.Interfaces;
using Domain.Common;
using Domain.DTO;
using Domain.Exceptions;
using Domain.Interface;
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
            Mock.Setup(x => x.GetPropertiesAll(It.IsAny<FiltroProperty>())).ThrowsAsync(new ApiException(exceptionMessage));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.GetAll(null, null, null, null, 1, 10);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Test exception message"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
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
            var request = new PropertyRequest { /* Initialize with valid data */ };        

            Mock.Setup(x => x.Create(request)).ThrowsAsync(new NotFoundException("El nombre del edificio es obligatorio."));
            // Act

            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _controller.Create(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("El nombre del edificio es obligatorio."));
            Assert.That(ex.StatusCode, Is.EqualTo(404));

        }

        [Test]
        public async Task Create_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            var request = new PropertyRequest { /* Initialize with valid data */ }; // Important!
            var exceptionMessage = "Test exception";
            Mock.Setup(x => x.Create(request)).ThrowsAsync(new ApiException(exceptionMessage));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Create(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));

        }


    }

}

