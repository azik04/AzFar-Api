﻿using Final.BLL.Services.Stadiums;
using Final.Domain.ViewModel.Stadiums;
using Microsoft.AspNetCore.Authorization;
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
        [Route("GetStadiums")]
        public IActionResult GetStadiums()
        {
            var response = _stadiumService.GetStadiums();
            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }

            return BadRequest($"{response.Description}");
        }

        [HttpGet]
        [Route("GetStadium")]
        public IActionResult GetStadium(int id)
        {
            var response = _stadiumService.GetStadium(id);
            return Ok(response);
       
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _stadiumService.DeleteStadium(id);
            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok();
            }
            return BadRequest($"{response.Description}");
        }
        [HttpPost]
        [Route("CreateStadium")]
        public async Task<IActionResult> Create([FromForm] StadiumViewModel viewModel)
        {
            var response = await _stadiumService.CreateStadium(viewModel);

            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
