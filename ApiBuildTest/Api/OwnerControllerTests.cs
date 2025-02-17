﻿using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using ApiRest.Controllers;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Domain.Interface;

namespace ApiBuildTest.Api
{
    [TestFixture]
    public class OwnerControllerTests
    {

        private Mock<IOwnerService> _mockOwnerService;
        private OwnerController _controller;

        [SetUp]
        public void Setup()
        {
            _mockOwnerService = new Mock<IOwnerService>();
            _controller = new OwnerController(_mockOwnerService.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkResult_WithOwners()
        {
            // Arrange
            var owners = new List<OwnerResponse> { new OwnerResponse(), new OwnerResponse() };  
            _mockOwnerService.Setup(x => x.GetAll()).ReturnsAsync(owners);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;             
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task GetAll_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            _mockOwnerService.Setup(x => x.GetAll()).ThrowsAsync(new ApiException("error"));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.GetAll();
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("error"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task Create_ReturnsOkResult_WhenOwnerCreated()
        {
            // Arrange
            var ownerRequest = new OwnerRequest(); // Populate with test data
            var baseResponse = new BaseResponse { StatusCode = HttpStatusCode.OK }; // Successful response
            _mockOwnerService.Setup(x => x.Create(ownerRequest)).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Create(ownerRequest);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var okResult = (ObjectResult)result;
            Assert.That(okResult, Is.EqualTo(result));
            Assert.IsNotNull(result);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task Create_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {

            // Arrange
            var ownerRequest = new OwnerRequest();           
            _mockOwnerService.Setup(x => x.Create(ownerRequest)).ThrowsAsync(new NotFoundException("El nombre del edificio es obligatorio."));
            // Act

            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _controller.Create(ownerRequest);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("El nombre del edificio es obligatorio."));
            Assert.That(ex.StatusCode, Is.EqualTo(404));        
                       
        }


        [Test]
        public async Task Create_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            var ownerRequest = new OwnerRequest();
            _mockOwnerService.Setup(x => x.Create(ownerRequest)).ThrowsAsync(new ApiException("error"));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Create(ownerRequest);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("error"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }


    }
}
