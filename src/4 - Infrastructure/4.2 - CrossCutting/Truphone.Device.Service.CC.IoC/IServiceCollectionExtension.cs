using Microsoft.Extensions.DependencyInjection;
using Truphone.Device.Service.Application.Services;
using Truphone.Device.Service.Application.Services.Interfaces;
using Truphone.Device.Service.Domain.Repositories;
using Truphone.Device.Service.Domain.Services;
using Truphone.Device.Service.Domain.Services.Interfaces;
using Truphone.Device.Service.Repository;

namespace Truphone.Device.Service.CC.IoC
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInternalApplication(this IServiceCollection services)
        {
            services.AddTransient<IDeviceApplicationService, DeviceApplicationService>();

            return services;
        }

        public static IServiceCollection AddInternalDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IDeviceDomainService, DeviceDomainService>();

            return services;
        }

        public static IServiceCollection AddInternalRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }
}