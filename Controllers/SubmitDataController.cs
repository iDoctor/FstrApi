using Microsoft.AspNetCore.Mvc;
using FstrApi.Structures;
using FstrApi.Models;

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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

            ImagesController imagesController = new ImagesController();
            var imagesIds = imagesController.LoadImages(pereval.images).Result;

            RouteController routeController = new RouteController();
            var newRouteResult = await routeController.SaveNewRoute(pereval, imagesIds/*new List<int>()*/);

            var badRequestResult = newRouteResult as BadRequestObjectResult;
            if (badRequestResult != null)
                return BadRequest(badRequestResult.Value?.ToString());

            var okRequestResult = newRouteResult as OkObjectResult;
            if (okRequestResult != null)
            {
                return okRequestResult.Value == null ? BadRequest("Отсутствуют данные!") : Ok(okRequestResult.Value.ToString());
            }

            return BadRequest("Запрос выполнен некорректно!");
        }

        /// <summary>
        /// Вывод информации о всех маршрутах (для пользователя??)
        /// </summary>
        /// <param name="user">Набор информации о пользователе</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PerevalAdded>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        //[Produces("application/json", "text/plain")]
        public async Task<IActionResult> GetAllData([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Заполнены не все поля!");

            RouteController routeController = new RouteController();
            var allRoutesResult = await routeController.GetAllRoutes(user);

            var badRequestResult = allRoutesResult as BadRequestObjectResult;
            if (badRequestResult != null)
                return BadRequest(badRequestResult.Value?.ToString());

            var okRequestResult = allRoutesResult as OkObjectResult;
            if (okRequestResult != null)
            {
                return okRequestResult.Value == null ? BadRequest("Отсутствуют данные!") : Ok(okRequestResult.Value as List<PerevalAdded>);
            }
            
            return BadRequest("Запрос выполнен некорректно!");
        }

        /// <summary>
        /// Вывод информации о маршруте по его ID
        /// </summary>
        /// <param name="id">id маршрута</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PerevalAdded), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetDataById(int id)
        {
            RouteController routeController = new RouteController();
            var routeResult = await routeController.GetRouteById(id);

            var badRequestResult = routeResult as BadRequestObjectResult;
            if (badRequestResult != null)
                return BadRequest(badRequestResult.Value?.ToString());

            var okRequestResult = routeResult as OkObjectResult;
            if (okRequestResult != null)
            {
                return okRequestResult.Value == null ? BadRequest("Отсутствуют данные!") : Ok(okRequestResult.Value as PerevalAdded);
            }

            return BadRequest("Запрос выполнен некорректно!");
        }

        /// <summary>
        /// Вывод статуса маршрута по его ID
        /// </summary>
        /// <param name="id">id маршрута</param>
        /// <returns></returns>
        [HttpGet("{id}/status")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetStatus(int id)
        {
            RouteController routeController = new RouteController();
            var routeResult = await routeController.GetRouteStatus(id);

            var badRequestResult = routeResult as BadRequestObjectResult;
            if (badRequestResult != null)
                return BadRequest(badRequestResult.Value?.ToString());

            var okRequestResult = routeResult as OkObjectResult;
            if (okRequestResult != null)
            {
                return okRequestResult.Value == null ? BadRequest("Отсутствуют данные!") : Ok(okRequestResult.Value.ToString());
            }

            return BadRequest("Запрос выполнен некорректно!");
        }

        /// <summary>
        /// Редактирование маршрута
        /// </summary>
        /// <param name="id">id маршрута</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PerevalAdded), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> EditData(int id)
        {
            RouteController routeController = new RouteController();
            var routeResult = await routeController.EditRoute(id);

            var badRequestResult = routeResult as BadRequestObjectResult;
            if (badRequestResult != null)
                return BadRequest(badRequestResult.Value?.ToString());

            var okRequestResult = routeResult as OkObjectResult;
            if (okRequestResult != null)
            {
                return okRequestResult.Value == null ? BadRequest("Отсутствуют данные!") : Ok(okRequestResult.Value as PerevalAdded);
            }

            return BadRequest("Запрос выполнен некорректно!");
        }
    }
}
