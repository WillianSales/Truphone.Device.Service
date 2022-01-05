using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Truphone.Device.Service.Domain.Repositories;
using Truphone.Device.Service.Domain.Services;
using Xunit;

namespace Truphone.Device.Service.Domain.UnitTest
{
    public class DeviceDomainServiceTests
    {
        [Fact]
        public async void ShouldCreateWithSuccess()
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var device = new Entities.Device
            {
                Name = "Test",
                Brand = "Test"
            };

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            var idDevice = Guid.NewGuid();

            deviceRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Entities.Device>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Entities.Device { Id = idDevice });

            // Act
            var response = await deviceDomainService.CreateAsync(device, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(idDevice, response.Id);
        }

        [Theory]
        [InlineData("", "Test")]
        [InlineData("Test", "")]
        public async void ShouldNotCreateWithInvalidFail(string name, string brand)
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var device = new Entities.Device
            {
                Name = name,
                Brand = brand
            };

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => deviceDomainService.CreateAsync(device, cancellationToken));
        }

        [Fact]
        public async void ShouldNotCreateWithDuplicateDeviceFail()
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var device = new Entities.Device
            {
                Name = "Test",
                Brand = "Test"
            };

            deviceRepositoryMock.Setup(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Entities.Page<Entities.Device>(new List<Entities.Device> { new Entities.Device() }, 0, 1, 1));

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            // Act
            // Assert
            await Assert.ThrowsAsync<ApplicationException>(() => deviceDomainService.CreateAsync(device, cancellationToken));
        }

        [Fact]
        public async void ShouldUpdateWithSuccess()
        {
            // Arrange
            var idDevice = Guid.NewGuid();

            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var device = new Entities.Device
            {
                Id = idDevice,
                Name = "Test",
                Brand = "Test"
            };

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            deviceRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(device);

            deviceRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Entities.Device>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(device);

            // Act
            var response = await deviceDomainService.UpdateAsync(device, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(idDevice, response.Id);
        }

        [Theory]
        [InlineData("", "Test")]
        [InlineData("Test", "")]
        public async void ShouldNotUpdateWithInvalidDeviceFail(string name, string brand)
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var device = new Entities.Device
            {
                Name = name,
                Brand = brand
            };

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => deviceDomainService.UpdateAsync(device, cancellationToken));
            deviceRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), cancellationToken), Times.Never);
            deviceRepositoryMock.Verify(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), cancellationToken), Times.Never);
            deviceRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Entities.Device>(), cancellationToken), Times.Never);
        }

        [Fact]
        public async void ShouldNotUpdateWithDeviceNotFoundFail()
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var device = new Entities.Device
            {
                Name = "Test",
                Brand = "Test"
            };
   
            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            // Act
            // Assert
            await Assert.ThrowsAsync<ApplicationException>(() => deviceDomainService.UpdateAsync(device, cancellationToken));
            deviceRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), cancellationToken), Times.Once);
            deviceRepositoryMock.Verify(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), cancellationToken), Times.Never);
            deviceRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Entities.Device>(), cancellationToken), Times.Never);
        }

        [Fact]
        public async void ShouldNotUpdateWithDuplicatedDeviceFail()
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var device = new Entities.Device
            {
                Name = "Test",
                Brand = "Test"
            };

            deviceRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Entities.Device());

            deviceRepositoryMock.Setup(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Entities.Page<Entities.Device>(new List<Entities.Device> { new Entities.Device() }, 0, 1, 1));

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            // Act
            // Assert
            await Assert.ThrowsAsync<ApplicationException>(() => deviceDomainService.UpdateAsync(device, cancellationToken));
            deviceRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), cancellationToken), Times.Once);
            deviceRepositoryMock.Verify(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), cancellationToken), Times.Once);
            deviceRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Entities.Device>(), cancellationToken), Times.Never);
        }

        [Fact]
        public async void ShouldGetPagedWithSuccess()
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            var idDevice = Guid.NewGuid();

            deviceRepositoryMock.Setup(x => x.GetPagedAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Entities.Page<Entities.Device>(new List<Entities.Device> { new Entities.Device() }, 0, 1, 1));

            // Act
            var response = await deviceDomainService.GetPagedAsync("", "", 0, 30, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Entries.Any());
        }

        [Fact]
        public async void ShouldGetByIdWithSuccess()
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();

            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            var idDevice = Guid.NewGuid();

            deviceRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Entities.Device { Id = idDevice });

            // Act
            var response = await deviceDomainService.GetByIdAsync(idDevice, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(idDevice, response.Id);
        }

        [Fact]
        public async void ShouldDeleteWithSuccess()
        {
            // Arrange
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var deviceDomainService = new DeviceDomainService(deviceRepositoryMock.Object);
            var cancellationToken = new CancellationToken();

            var idDevice = Guid.NewGuid();

            // Act
            await deviceDomainService.DeleteAsync(idDevice, cancellationToken);

            // Assert
            deviceRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), cancellationToken), Times.Once());
        }
    }
}