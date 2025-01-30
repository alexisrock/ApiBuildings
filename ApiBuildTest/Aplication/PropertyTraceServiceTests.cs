using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Aplication.Services;
using Domain.DTO;
using Domain.Entities;
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
            _mockRepository.Setup(x => x.GetPropertyTraceById(request.IdPropertyTrace)).ReturnsAsync(obj);  

            // Act
            var result = await _service.Update(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(result.Message, Is.EqualTo("Bad Request"));
        }

        [Test]
        public async Task Update_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = new PropertyTraceUpdateRequest { IdPropertyTrace = 1,   };
            var exceptionMessage = "Test exception";

            _mockRepository.Setup(x => x.GetPropertyTraceById(request.IdPropertyTrace)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _service.Update(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(result.Message, Is.EqualTo(exceptionMessage));
        }

        
    }
}
