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
    public class PropertyImagesControllerTest
    {


        private Mock<IPropertyImageService> Mock;
        private PropertyImagesController _controller;


        [SetUp]
        public void Setup()
        {
            Mock = new Mock<IPropertyImageService>();
            _controller = new PropertyImagesController(Mock.Object);
        }


        [Test]
        public async Task Update_ReturnsOk_WhenPropertyImagesUpdated()
        {
            // Arrange
            var request = new PropertyImagesUpdateRequest {   };  
            var baseResponse = new BaseResponse { StatusCode = HttpStatusCode.OK,   };
            Mock.Setup(x => x.Update(request)).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Update(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(okResult.Value, Is.EqualTo(baseResponse));  
        }

        [Test]
        public async Task Update_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {
            // Arrange
            var request = new PropertyImagesUpdateRequest {  };
            Mock.Setup(x => x.Update(request)).ThrowsAsync(new NotFoundException("Error"));

            // Act
            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _controller.Update(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Error"));
            Assert.That(ex.StatusCode, Is.EqualTo(404));


           
        }

        [Test]
        public async Task Update_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            var request = new PropertyImagesUpdateRequest {   };           
            var exceptionMessage = "Test exception message";

            Mock.Setup(x => x.Update(request)).ThrowsAsync(new ApiException(exceptionMessage));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Update(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Test exception message"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));

        }


    }
}
