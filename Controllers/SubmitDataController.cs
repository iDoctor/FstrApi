using Microsoft.AspNetCore.Mvc;
using FstrApi.Structures;

namespace FstrApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitDataController : Controller
    {
        /// <summary>
        /// Добавление нового маршрута
        /// </summary>
        /// <param name="pereval">Набор информации о маршруте</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> AddNewData([FromBody] Pereval pereval)
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

            //ImagesController imagesController = new ImagesController();
            //var imagesIds = imagesController.LoadImages(pereval.images).Result;

            return Ok(pereval);
        }
    }
}
