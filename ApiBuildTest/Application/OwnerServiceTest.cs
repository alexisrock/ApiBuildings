using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using AutoMapper;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiBuildTest.Aplication
{
    [TestFixture]
    public class OwnerServiceTest
    {
        private Mock<IRepository<Owner>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<OwnerService>> _mockLogger;
        private OwnerService _ownerService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Owner>>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<OwnerService>>();
            _ownerService = new OwnerService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetAll_ReturnsListOfOwnerResponse()
        {
            // Arrange
            var owners = new List<Owner> { new Owner(), new Owner() }; 
            var ownerResponses = new List<OwnerResponse> { new OwnerResponse(), new OwnerResponse() };  

            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync(owners);
            _mockMapper.Setup(x => x.Map<List<OwnerResponse>>(owners)).Returns(ownerResponses);  

            // Act
            var result = await _ownerService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<OwnerResponse>>(result);
            Assert.That(result.Count, Is.EqualTo(ownerResponses.Count));  
        }

        [Test]
        public async Task Create_ReturnsOkResponse_WhenOwnerCreated()
        {
            // Arrange
            var request = new OwnerRequest {   };  
            var owner = new Owner();  
             

            _mockMapper.Setup(x => x.Map<Owner>(request)).Returns(owner);
            _mockRepository.Setup(x => x.Insert(owner)).Returns(Task.CompletedTask);  

            // Act
            var result = await _ownerService.Create(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(result?.Message?.Contains("created"));  
        }

        [Test]
        public async Task Create_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Arrange
           

            var request = new OwnerRequest { };
            var owner = new Owner();
            _mockMapper.Setup(x => x.Map<Owner>(request)).Returns(owner);
            _mockRepository.Setup(x => x.Insert(owner)).ThrowsAsync(new NotFoundException("error"));

                    
            // Act
            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _ownerService.Create(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("error"));
            Assert.That(ex.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Create_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = new OwnerRequest {   };
            var owner = new Owner();
            var exceptionMessage = "Test exception";
             
            _mockMapper.Setup(x => x.Map<Owner>(request)).Returns(owner);
            _mockRepository.Setup(x => x.Insert(owner)).ThrowsAsync(new ApiException(exceptionMessage));
            
            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _ownerService.Create(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }



    }
}
