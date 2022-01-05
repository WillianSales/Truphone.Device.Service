using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Truphone.Device.Service.Application.DTO;
using Truphone.Device.Service.Application.Services.Interfaces;

namespace Truphone.Device.Service.API.Controllers
{
    [ApiController]
    [Route("devices")]
    public class DevicesController : Controller
    {
        private readonly IDeviceApplicationService _deviceService;

        public DevicesController(IDeviceApplicationService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Get a list of devices.
        /// </summary>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
        /// <param name="brand">Device brand</param>
        /// <param name="pageIndex">Devices list page index</param>
        /// <param name="pageSize">Devices list page size</param>
        /// <returns>Devices list</returns>
        [HttpGet]
        public async Task<ActionResult<PageDto<DeviceDto>>> GetPagedAsync(
            CancellationToken cancellationToken,
            string? name = "",
            string? brand = "",
            int pageIndex = 0,
            int pageSize = 30)
        {
            return this.Ok(await _deviceService.GetPagedAsync(name, brand, pageIndex, pageSize, cancellationToken));
        }

        /// <summary>
        /// Get a device by id.
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
        /// <returns>Device consulted</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IList<DeviceDto>>> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var device = await _deviceService.GetByIdAsync(id, cancellationToken);

            if (device == null)
                return this.NotFound($"Device with Id = {id} not found.");

            return this.Ok(device);
        }

        /// <summary>
        /// Create device.
        /// </summary>
        /// <param name="device">Device to be created</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
        /// <returns>Created device</returns>
        [HttpPost]
        public async Task<ActionResult<DeviceDto>> PostAsync([FromBody] DeviceDto device, CancellationToken cancellationToken)
        {
            try
            {
                return this.Ok(await _deviceService.CreateAsync(device, cancellationToken));
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updated device.
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <param name="device">Device to be updated</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
        /// <returns>Updated device</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<DeviceDto>> PutAsync(Guid id, DeviceDto device, CancellationToken cancellationToken)
        {
            try
            {
                device.Id = id;
                return this.Ok(await _deviceService.UpdateAsync(device, cancellationToken));
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update the device-specific field. How to use: http://jsonpatch.com/
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <param name="patchDoc">Json with the device-specific field to be patched</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
        /// <returns>Patched device</returns>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<DeviceDto>> PatchAsync(Guid id, [FromBody] JsonPatchDocument<DeviceDto> patchDoc, CancellationToken cancellationToken)
        {
            try
            {
                var device = await _deviceService.GetByIdAsync(id, cancellationToken) ?? new DeviceDto();

                var creationTime = device.CreationTime;

                patchDoc.ApplyTo(device, ModelState);

                device.Id = id;
                device.CreationTime = creationTime;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return this.Ok(await _deviceService.UpdateAsync(device, cancellationToken));
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete device.
        /// </summary>
        /// <param name="id">Device identifier</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
        /// <returns>NoContent</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _deviceService.DeleteAsync(id, cancellationToken);

            return this.NoContent();
        }
    }
}