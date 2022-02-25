using Microsoft.AspNetCore.Mvc;

namespace FstrApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitDataController : Controller
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public IActionResult AddNewData([FromBody] Structures.Pereval pereval)
        {
            if (!ModelState.IsValid)
                return BadRequest("Заполнены не все поля!");

            if (pereval == null
                || (string.IsNullOrEmpty(pereval.level.winter)
                && string.IsNullOrEmpty(pereval.level.summer)
                && string.IsNullOrEmpty(pereval.level.autumn)
                && string.IsNullOrEmpty(pereval.level.spring))
                || pereval.images.Count == 0)
                return BadRequest("Заполнены не все поля!");

            return Ok(pereval);
        }
    }
}
