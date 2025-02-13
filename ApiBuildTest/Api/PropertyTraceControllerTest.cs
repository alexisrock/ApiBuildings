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
    public class PropertyTraceControllerTest
    {



        private Mock<IPropertyTraceService> Mock;
        private PropertyTraceController _controller;


        [SetUp]
        public void Setup()
        {
            Mock = new Mock<IPropertyTraceService>();
            _controller = new PropertyTraceController(Mock.Object);
        }


        [Test]
        public async Task Update_ReturnsOk_WhenPropertyTraceUpdated()
        {
            // Arrange
            var request = new PropertyTraceUpdateRequest {   };  
            var baseResponse = new BaseResponse { StatusCode = HttpStatusCode.OK,   };
            Mock.Setup(x => x.Update(request)).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Update(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(okResult.Value, Is.EqualTo(baseResponse)); // Check the returned BaseResponse
        }

        [Test]
        public async Task Update_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {
            // Arrange
            var request = new PropertyTraceUpdateRequest {   };  
            Mock.Setup(x => x.Update(request)).ThrowsAsync(new NotFoundException("error"));
            // Act

            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _controller.Update(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("error"));
            Assert.That(ex.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Update_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            var request = new PropertyTraceUpdateRequest {   };
            var exceptionMessage = "Test exception";
            Mock.Setup(x => x.Update(request)).ThrowsAsync(new ApiException(exceptionMessage));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Update(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));


        }

    }
}
