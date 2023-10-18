using Final.BLL.Services.OrderTimes;
using Final.BLL.Services.Stadiums;
using Final.Domain.Entity;
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
        [HttpGet]
        public IActionResult GetStadiums()
        {
            var response = _stadiumService.GetStadiums();
            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<ActionResult> GetStadium(int id)
        {
            var response = await _stadiumService.GetStadium(id);
            return Ok(response.Data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _stadiumService.DeleteStadium(id);
            return RedirectToAction("GetCars");
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateStadium(Stadium model)
        //{
        //    ModelState.Remove("Id");
        //    ModelState.Remove("DateCreate");
        //    if (ModelState.IsValid)
        //    {
        //        if (model.Id == 0)
        //        {
        //            byte[] imageData;
        //            using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
        //            {
        //                imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
        //            }
        //            await _stadiumService.CreateStadium(model, imageData);
        //        }
        //    }
        //    return RedirectToAction("GetCars");
        //}
    }
}
