using Microsoft.AspNetCore.Mvc;
using SnapSoft.DataHandler;
using SnapSoft.Models;
using SnapSoft.Services;

namespace SnapSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculateController : ControllerBase
    {
        private ICalculateService _calculateService;
        public CalculateController(ICalculateService calculateService)
        {
            _calculateService = calculateService;
        }

        [HttpPost]
        [Route("a")]
        public IActionResult Get([FromBody] RequestBody requestBody)
        {
            CheckRequest(requestBody);
            return Ok(_calculateService.GetA(requestBody));
        }

        [HttpPost]
        [Route("b")]
        public IActionResult GetB([FromBody] RequestBody requestBody)
        {
            CheckRequest(requestBody);    
            return Ok(_calculateService.GetB(requestBody));
        }

        [HttpPost]
        [Route("c")]
        public IActionResult GetC([FromBody] RequestBody requestBody)
        {
            CheckRequest(requestBody);
            return Ok(_calculateService.GetC(requestBody));
        }


        [HttpGet]
        [Route("/api/history")]
        public IActionResult Test([FromQuery] Filter? filter)
        {
            return Ok(_calculateService.ListAll(filter));
        }



        private void CheckRequest(RequestBody requestBody)
        {
            if (requestBody == null || requestBody.array is null) throw new ArgumentException("You must pass an array");
            int[] array = requestBody.array;
            if (array.Length < 3) throw new ArgumentException("The array size must be larger than 2!");
        }

    }
}