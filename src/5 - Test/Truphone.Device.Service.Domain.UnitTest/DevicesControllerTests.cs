using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Truphone.Device.Service.API.Controllers;
using Truphone.Device.Service.Application.Services.Interfaces;
using Xunit;

namespace Truphone.Device.Service.Domain.UnitTest
{
    public class DevicesControllerTests
    {
        [Fact]
        public async void ShouldGetPagedAsyncWithSuccess()
        {
            // Arrange
            var deviceApplicationServiceMock = new Mock<IDeviceApplicationService>();

            var devicesController = new DevicesController(deviceApplicationServiceMock.Object);
            var cancellationToken = new CancellationToken();

            deviceApplicationServiceMock.Setup(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Application.DTO.PageDto<Application.DTO.DeviceDto> { Entries = new List<Application.DTO.DeviceDto> { new Application.DTO.DeviceDto() } });

            // Act
            var response = await devicesController.GetPagedAsync(cancellationToken, "", "", 0, 30);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.IsType<Application.DTO.PageDto<Application.DTO.DeviceDto>>(((ObjectResult)response.Result).Value);
            deviceApplicationServiceMock.Verify(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void ShouldGetAsyncWithSuccess()
        {
            // Arrange
            var deviceApplicationServiceMock = new Mock<IDeviceApplicationService>();
            
            var devicesController = new DevicesController(deviceApplicationServiceMock.Object);
            var cancellationToken = new CancellationToken();

            deviceApplicationServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Application.DTO.DeviceDto());

            // Act
            var response = await devicesController.GetAsync(Guid.NewGuid(), cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.IsType<Application.DTO.DeviceDto>(((ObjectResult)response.Result).Value);
            deviceApplicationServiceMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void ShouldGetAsyncWithNotFoundFail()
        {
            // Arrange
            var deviceApplicationServiceMock = new Mock<IDeviceApplicationService>();

            var devicesController = new DevicesController(deviceApplicationServiceMock.Object);
            var cancellationToken = new CancellationToken();

            // Act
            var response = await devicesController.GetAsync(Guid.NewGuid(), cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NotFoundObjectResult>(response.Result);
            deviceApplicationServiceMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void ShouldPostWithSuccess()
        {
            // Arrange
            var deviceApplicationServiceMock = new Mock<IDeviceApplicationService>();
            var device = new Application.DTO.DeviceDto
            {
                Name = "Test",
                Brand = "Test"
            };

            var devicesController = new DevicesController(deviceApplicationServiceMock.Object);
            var cancellationToken = new CancellationToken();

            var idDevice = Guid.NewGuid();

            deviceApplicationServiceMock.Setup(x => x.CreateAsync(It.IsAny<Application.DTO.DeviceDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Application.DTO.DeviceDto { Id = idDevice });

            // Act
            var response = await devicesController.PostAsync(device, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.IsType<Application.DTO.DeviceDto>(((ObjectResult)response.Result).Value);
            deviceApplicationServiceMock.Verify(x => x.CreateAsync(It.IsAny<Application.DTO.DeviceDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void ShouldPutWithSuccess()
        {
            // Arrange
            var deviceApplicationServiceMock = new Mock<IDeviceApplicationService>();
            var device = new Application.DTO.DeviceDto
            {
                Name = "Test",
                Brand = "Test"
            };

            var devicesController = new DevicesController(deviceApplicationServiceMock.Object);
            var cancellationToken = new CancellationToken();

            var idDevice = Guid.NewGuid();

            deviceApplicationServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Application.DTO.DeviceDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Application.DTO.DeviceDto { Id = idDevice });

            // Act
            var response = await devicesController.PutAsync(idDevice, device, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.IsType<Application.DTO.DeviceDto>(((ObjectResult)response.Result).Value);
            deviceApplicationServiceMock.Verify(x => x.UpdateAsync(It.IsAny<Application.DTO.DeviceDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        //[Fact]
        //public async void ShouldPatchWithSuccess()
        //{
        //    // Arrange
        //    var deviceApplicationServiceMock = new Mock<IDeviceApplicationService>();
        //    var device = new Application.DTO.DeviceDto
        //    {
        //        Name = "Test",
        //        Brand = "Test"
        //    };

        //    var devicesController = new DevicesController(deviceApplicationServiceMock.Object);
        //    var cancellationToken = new CancellationToken();

        //    var idDevice = Guid.NewGuid();

        //    deviceApplicationServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Application.DTO.DeviceDto>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new Application.DTO.DeviceDto { Id = idDevice });

        //    // Act
        //    var response = await devicesController.PatchAsync(idDevice, device, cancellationToken);

        //    // Assert
        //    Assert.NotNull(response);
        //    Assert.IsType<OkObjectResult>(response.Result);
        //    Assert.IsType<Application.DTO.DeviceDto>(((ObjectResult)response.Result).Value);
        //    deviceApplicationServiceMock.Verify(x => x.UpdateAsync(It.IsAny<Application.DTO.DeviceDto>(), It.IsAny<CancellationToken>()), Times.Once);
        //}

        [Fact]
        public async void ShouldDeleteWithSuccess()
        {
            // Arrange
            var deviceApplicationServiceMock = new Mock<IDeviceApplicationService>();

            var devicesController = new DevicesController(deviceApplicationServiceMock.Object);
            var cancellationToken = new CancellationToken();

            var idDevice = Guid.NewGuid();

            // Act
            var response = await devicesController.DeleteAsync(idDevice, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response);
            deviceApplicationServiceMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}