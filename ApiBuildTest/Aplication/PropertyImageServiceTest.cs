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
    public class PropertyImageServiceTests
    {
        private Mock<IPropertyImagesRepository> _mockRepository;
        private Mock<ILogger<PropertyImageService>> _mockLogger;
        private PropertyImageService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyImagesRepository>();
            _mockLogger = new Mock<ILogger<PropertyImageService>>();
            _service = new PropertyImageService(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Update_ReturnsOk_WhenPropertyImageUpdated()
        {
            // Arrange
            var request = new PropertyImagesUpdateRequest { IdPropertyImage = 1,  };  
            var propertyImage = new PropertyImage { IdPropertyImage = 1,   };  
              

            _mockRepository.Setup(x => x.GetPropertyImageById(request.IdPropertyImage)).ReturnsAsync(propertyImage);
            _mockRepository.Setup(x => x.UpdatePropertyImage(It.IsAny<PropertyImage>())).Returns(Task.CompletedTask); // Successful update

            // Act
            var result = await _service.Update(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result?.Message?.Contains("update"), Is.True);  
        }

        [Test]
        public async Task Update_ReturnsBadRequest_WhenPropertyImageNotFound()
        {
            // Arrange
            var request = new PropertyImagesUpdateRequest { IdPropertyImage = 1 };  

            _mockRepository.Setup(x => x.GetPropertyImageById(request.IdPropertyImage)).ReturnsAsync((PropertyImage)null); // Image not found

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
            var request = new PropertyImagesUpdateRequest { IdPropertyImage = 1,  };
            var exceptionMessage = "Test exception";

            _mockRepository.Setup(x => x.GetPropertyImageById(request.IdPropertyImage)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _service.Update(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(result.Message, Is.EqualTo(exceptionMessage));
        }

        
    }
}
