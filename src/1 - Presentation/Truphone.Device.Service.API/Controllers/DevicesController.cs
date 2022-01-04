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

        [HttpGet]
        public async Task<ActionResult<PageDto<DeviceDto>>> GetPagedAsync(string brand, int pageIndex = 0, int pageSize = 30)
        {
            return this.Ok(await _deviceService.GetPagedAsync(brand, pageIndex, pageSize));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IList<DeviceDto>>> GetAsync(Guid id)
        {
            return this.Ok(await _deviceService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<DeviceDto>> PostAsync([FromBody] DeviceDto device)
        {
            return this.Ok(await _deviceService.CreateAsync(device));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<DeviceDto>> PutAsync(Guid id, DeviceDto device)
        {
            device.Id = id;
            return this.Ok(await _deviceService.UpdateAsync(device));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<DeviceDto>> PatchAsync(Guid id, [FromBody] JsonPatchDocument<DeviceDto> patchDoc)
        {
            var device = await _deviceService.GetByIdAsync(id) ?? new DeviceDto();

            patchDoc.ApplyTo(device, ModelState);

            device.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return this.Ok(await _deviceService.UpdateAsync(device));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _deviceService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}