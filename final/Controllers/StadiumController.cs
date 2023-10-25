using Final.BLL.Services.OrderTimes;
using Final.BLL.Services.Stadiums;
using Final.Domain.Entity;
using Final.Domain.ViewModel.Stadiums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private readonly IStadiumService _stadiumService;

        public StadiumController(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetStadiums()
        {
            var response = _stadiumService.GetStadiums();
            return Ok(response.Data);
        }

        [HttpGet]
        [Route("GetStadium")]
        public IActionResult GetStadium(int id)
        {
            var response =  _stadiumService.GetStadium(id);
            return Ok(response);
       
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _stadiumService.DeleteStadium(id);
            return RedirectToAction("GetStadiums");
        }
        [HttpPost]
        public async Task<IActionResult> CreateStadium(StadiumViewModel model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _stadiumService.CreateStadium(model, imageData);
                }
            }
            return RedirectToAction("GetStadiums");
        }
    }
}
