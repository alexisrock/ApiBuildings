using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interface;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiBuildTest.Aplication
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IPropertyRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<PropertyService>> _mockLogger;
        private PropertyService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<PropertyService>>();
            _service = new PropertyService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetPropertiesAll_ReturnsListOfPropertyResponse()
        {
            // Arrange
            var filtro = new FiltroProperty(); // You can customize the filter as needed
            var properties = new List<Property> { new Property(), new Property() };
            var propertyResponses = new List<PropertyResponse> { new PropertyResponse(), new PropertyResponse() };

            _mockRepository.Setup(x => x.GetPropertyAll(It.IsAny<Expression<Func<Property, bool>>>(), filtro.Pagina, filtro.TamanioPagina)).ReturnsAsync(properties);
            _mockMapper.Setup(x => x.Map<List<PropertyResponse>>(properties)).Returns(propertyResponses);

            // Act
            var result = await _service.GetPropertiesAll(filtro);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<PropertyResponse>>(result);
            Assert.AreEqual(propertyResponses.Count, result.Count);
        }

        [Test]
        public async Task Create_ReturnsOkResponse_WhenPropertyCreated()
        {
            // Arrange
            var request = new PropertyRequest {   }; 
            var property = new Property();
            var propertyImages = new PropertyImage();
            var propertyTrace = new PropertyTrace();

            _mockMapper.Setup(x => x.Map<Property>(request)).Returns(property);
            _mockMapper.Setup(x => x.Map<PropertyImage>(request)).Returns(propertyImages);
            _mockMapper.Setup(x => x.Map<PropertyTrace>(request)).Returns(propertyTrace);
            _mockRepository.Setup(x => x.GetTransaction(property, propertyImages, propertyTrace)).ReturnsAsync(true);

            // Act
            var result = await _service.Create(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(result?.Message?.Contains("create"));
        }

        [Test]
        public async Task Create_ReturnsBadRequest_WhenTransactionFails()
        {
            // Arrange
            var request = new PropertyRequest {   };  
            var property = new Property();
            var propertyImages = new PropertyImage();
            var propertyTrace = new PropertyTrace();

            _mockMapper.Setup(x => x.Map<Property>(request)).Returns(property);
            _mockMapper.Setup(x => x.Map<PropertyImage>(request)).Returns(propertyImages);
            _mockMapper.Setup(x => x.Map<PropertyTrace>(request)).Returns(propertyTrace);
            _mockRepository.Setup(x => x.GetTransaction(property, propertyImages, propertyTrace)).ThrowsAsync(new NotFoundException("error"));// Transaction fails

            
            // Act
            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _service.Create(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("error"));
            Assert.That(ex.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Create_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = new PropertyRequest {  };  
            var exceptionMessage = "Test exception";

            _mockMapper.Setup(x => x.Map<Property>(request)).Throws(new ApiException(exceptionMessage));

            // Act
            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _service.Create(request);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }

         

        [Test]
        public async Task Update_ReturnsOkResponse_WhenPropertyUpdated()
        {
            // Arrange
            var request = new PropertyUpdateRequest { IdProperty = 1, };  
            var property = new Property { IdProperty = 1,   };  
           

            _mockRepository.Setup(x => x.GetPropertyById(request.IdProperty)).ReturnsAsync(property);
            _mockRepository.Setup(x => x.updateProperty(It.IsAny<Property>())).Returns(Task.CompletedTask);  

            // Act
            var result = await _service.Update(request);            


            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(result?.Message?.Contains("update"));  
        }

        [Test]
        public async Task Update_ReturnsBadRequest_WhenPropertyNotFound()
        {
            // Arrange
            var request = new PropertyUpdateRequest { IdProperty = 1 }; // Valid ID, but property not found

            _mockRepository.Setup(x => x.GetPropertyById(request.IdProperty)).ThrowsAsync(new NotFoundException("error")); // property not found

           
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
        public async Task Update_ReturnsInternalServerError_WhenExceptionThrownUpdate()
        {
            // Arrange
            var request = new PropertyUpdateRequest { IdProperty = 1, /* ... other valid data */ };
            var exceptionMessage = "Test exception";

            _mockRepository.Setup(x => x.GetPropertyById(request.IdProperty)).ThrowsAsync(new ApiException(exceptionMessage));

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
