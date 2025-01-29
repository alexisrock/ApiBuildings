//using Aplication.Interfaces;
//using Moq;
//using Domain.DTO;
//using ApiRest.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Domain.Common;



//namespace Tests.Presentacion
//{
//    //public class ProductoControllerTest
//    {

//        private readonly Mock<IProductoService> _pruebaSeleccionServiceMock;
//    private readonly ProductoController _controller;
//    public ProductoControllerTest()
//    {
//        _pruebaSeleccionServiceMock = new Mock<IProductoService>();
//        _controller = new ProductoController(_pruebaSeleccionServiceMock.Object);

//    }

//    [Fact]
//    public async Task GetAll_ShouldReturnAllProducts()
//    {
//        var list = new List<ProductoResponse>() {new ProductoResponse {   Name = "arroz", Description = "arroz", Price = 13.46m , Stock= 1000 },
//                new ProductoResponse { Name = "papa", Description = "papa", Price = 13.46m , Stock= 1000 } };



//        _pruebaSeleccionServiceMock.Setup(x => x.GetAll()).ReturnsAsync(list);
//        var result = await _controller.GetAll();

//        var okResult = Assert.IsType<OkObjectResult>(result);
//        Assert.Equal(200, okResult.StatusCode);


//    }


//    [Fact]
//    public async Task GetAll_ReturnsProblem_WhenServiceThrowsException()
//    {

//        _pruebaSeleccionServiceMock.Setup(x => x.GetAll()).ThrowsAsync(new System.Exception());


//        var result = await _controller.GetAll();


//        Assert.IsType<ObjectResult>(result);
//        var objectResult = result as ObjectResult;
//        Assert.Equal(500, objectResult.StatusCode);
//    }


//    [Fact]
//    public async Task GetId_ReturnsOk_WhenServiceReturnsProduct()
//    {

//        var productoMock = new ProductoResponse { Name = "arroz", Description = "arroz", Price = 13.46m, Stock = 1000 };


//        _pruebaSeleccionServiceMock.Setup(x => x.GetId(1)).ReturnsAsync(productoMock);


//        var result = await _controller.GetId(1);


//        var okResult = Assert.IsType<OkObjectResult>(result);
//        Assert.Equal(200, okResult.StatusCode);
//        Assert.Equal(productoMock, okResult.Value);
//    }

//    [Fact]
//    public async Task GetId_ReturnsBadRequest_WhenServiceReturnsNull()
//    {

//        _pruebaSeleccionServiceMock.Setup(service => service.GetId(1)).ReturnsAsync((ProductoResponse)null);


//        var result = await _controller.GetId(1);


//        var badRequestResult = Assert.IsType<BadRequestResult>(result);
//        Assert.Equal(400, badRequestResult.StatusCode);
//    }

//    [Fact]
//    public async Task GetId_ReturnsProblem_WhenServiceThrowsException()
//    {

//        _pruebaSeleccionServiceMock.Setup(service => service.GetId(1)).ThrowsAsync(new System.Exception());


//        var result = await _controller.GetId(1);


//        var objectResult = Assert.IsType<ObjectResult>(result);
//        Assert.Equal(500, objectResult.StatusCode);
//    }

//    [Fact]
//    public async Task Create_ReturnsOk_WhenServiceReturnsResult()
//    {
//        var productoMock = new BaseResponse { Mensaje = "Producto Creado" };
//        var product = new OwnerRequest { Name = "arroz", Description = "arroz", Price = 13.46m, Stock = 1000 };

//        _pruebaSeleccionServiceMock.Setup(x => x.Create(product)).ReturnsAsync(productoMock);

//        var result = await _controller.Create(product);

//        var okResult = Assert.IsType<OkObjectResult>(result);
//        Assert.Equal(200, okResult.StatusCode);
//        Assert.Equal(productoMock, okResult.Value);

//    }

//    [Fact]
//    public async Task Create_ReturnsBadRequest_WhenServiceReturnsNull()
//    {

//        var product = new OwnerRequest { Name = "arroz", Description = "arroz", Price = 13.46m, Stock = 1000 };

//        _pruebaSeleccionServiceMock.Setup(x => x.Create(product)).ReturnsAsync((BaseResponse)null);

//        var result = await _controller.Create(product);

//        var badRequestResult = Assert.IsType<BadRequestResult>(result);
//        Assert.Equal(400, badRequestResult.StatusCode);

//    }


//    [Fact]
//    public async Task Create_ReturnsProblem_WhenServiceThrowsException()
//    {

//        var requestMock = new OwnerRequest { Name = "arroz", Description = "arroz", Price = 13.46m, Stock = 1000 };
//        _pruebaSeleccionServiceMock.Setup(service => service.Create(requestMock)).ThrowsAsync(new System.Exception());


//        var result = await _controller.Create(requestMock);


//        var objectResult = Assert.IsType<ObjectResult>(result);
//        Assert.Equal(500, objectResult.StatusCode);
//    }



//    [Fact]
//    public async Task Update_ReturnsOk_WhenServiceReturnsResult()
//    {
//        var productoMock = new BaseResponse { Mensaje = "Producto Creado" };
//        var product = new ProductoUpdateRequest { Id = 1, Name = "arroz", Description = "arroz", Price = 13.46m, Stock = 1000 };

//        _pruebaSeleccionServiceMock.Setup(x => x.Update(product)).ReturnsAsync(productoMock);

//        var result = await _controller.Update(product);

//        var okResult = Assert.IsType<OkObjectResult>(result);
//        Assert.Equal(200, okResult.StatusCode);
//        Assert.Equal(productoMock, okResult.Value);

//    }

//    [Fact]
//    public async Task Update_ReturnsBadRequest_WhenServiceReturnsNull()
//    {

//        var product = new ProductoUpdateRequest { Id = 1, Name = "arroz", Description = "arroz", Price = 13.46m, Stock = 1000 };

//        _pruebaSeleccionServiceMock.Setup(x => x.Update(product)).ReturnsAsync((BaseResponse)null);

//        var result = await _controller.Update(product);

//        var badRequestResult = Assert.IsType<BadRequestResult>(result);
//        Assert.Equal(400, badRequestResult.StatusCode);

//    }


//    [Fact]
//    public async Task Update_ReturnsProblem_WhenServiceThrowsException()
//    {

//        var product = new ProductoUpdateRequest { Id = 1, Name = "arroz", Description = "arroz", Price = 13.46m, Stock = 1000 };

//        _pruebaSeleccionServiceMock.Setup(service => service.Update(product)).ThrowsAsync(new System.Exception());


//        var result = await _controller.Update(product);

//        var objectResult = Assert.IsType<ObjectResult>(result);
//        Assert.Equal(500, objectResult.StatusCode);
//    }




//    [Fact]
//    public async Task Delete_ReturnsOk_WhenServiceReturnsResult()
//    {
//        var productoMock = new BaseResponse { Mensaje = "Producto eliminado" };
//        int product = 1;

//        _pruebaSeleccionServiceMock.Setup(x => x.Delete(product)).ReturnsAsync(productoMock);

//        var result = await _controller.Delete(product);

//        var okResult = Assert.IsType<OkObjectResult>(result);
//        Assert.Equal(200, okResult.StatusCode);
//        Assert.Equal(productoMock, okResult.Value);

//    }

//    [Fact]
//    public async Task Delete_ReturnsBadRequest_WhenServiceReturnsNull()
//    {

//        int product = 1;

//        _pruebaSeleccionServiceMock.Setup(x => x.Delete(product)).ReturnsAsync((BaseResponse)null);

//        var result = await _controller.Delete(product);

//        var badRequestResult = Assert.IsType<BadRequestResult>(result);
//        Assert.Equal(400, badRequestResult.StatusCode);

//    }


//    [Fact]
//    public async Task Delete_ReturnsProblem_WhenServiceThrowsException()
//    {

//        int product = 1;

//        _pruebaSeleccionServiceMock.Setup(service => service.Delete(product)).ThrowsAsync(new System.Exception());


//        var result = await _controller.Delete(product);


//        var objectResult = Assert.IsType<ObjectResult>(result);
//        Assert.Equal(500, objectResult.StatusCode);
//    }



//}





//}

