using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interface;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiBuildTest.Aplication
{
    [TestFixture]
    public class PropertyTraceServiceTests
    {
        private Mock<IPropertyTraceRepository> _mockRepository;
        private Mock<ILogger<PropertyTraceService>> _mockLogger;
        private PropertyTraceService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyTraceRepository>();
            _mockLogger = new Mock<ILogger<PropertyTraceService>>();
            _service = new PropertyTraceService(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Update_ReturnsOk_WhenPropertyTraceUpdated()
        {
            // Arrange
            var request = new PropertyTraceUpdateRequest { IdPropertyTrace = 1,   }; 
            var propertyTrace = new PropertyTrace { IdPropertyTrace = 1,   };  
             

            _mockRepository.Setup(x => x.GetPropertyTraceById(request.IdPropertyTrace)).ReturnsAsync(propertyTrace);
            _mockRepository.Setup(x => x.UpdatePropertyTrace(It.IsAny<PropertyTrace>())).Returns(Task.CompletedTask); // Successful update

            // Act
            var result = await _service.Update(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(result?.Message?.Contains("update"));  
        }

        [Test]
        public async Task Update_ReturnsBadRequest_WhenPropertyTraceNotFound()
        {
            // Arrange
            var request = new PropertyTraceUpdateRequest { IdPropertyTrace = 1 };
            PropertyTrace? obj = null;
            _mockRepository.Setup(x => x.GetPropertyTraceById(request.IdPropertyTrace)).ThrowsAsync(new NotFoundException("error"));

             
            // Act
            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _service.Update(request);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo("error"));
            Assert.That(ex.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Update_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = new PropertyTraceUpdateRequest { IdPropertyTrace = 1,   };
            var exceptionMessage = "Test exception";

            _mockRepository.Setup(x => x.GetPropertyTraceById(request.IdPropertyTrace)).ThrowsAsync(new ApiException(exceptionMessage));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _service.Update(request);
            });
 

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }

        
    }
}
