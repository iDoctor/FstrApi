using FstrApi.Models;
using FstrApi.Structures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FstrApi.Controllers
{
    public class RouteController : Controller
    {
        public async Task<IActionResult> SaveNewRoute(Pereval pereval, List<int> imagesIds)
        {
            try
            {
                await using (FSTR_DBContext fstr = new FSTR_DBContext())
                {
                    var newRoute = new PerevalAdded
                    {
                        DateAdded = DateTime.Now,
                        RawData = JsonConvert.SerializeObject(pereval),
                        Images = "{}",  //imagesIds (convert to json)
                        Status = "new"
                    };

                    fstr.PerevalAddeds.Add(newRoute);

                    await fstr.SaveChangesAsync();

                    return Ok(newRoute.Id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException?.Message);
            }
        }

        public async Task<IActionResult> GetAllRoutes(User user)
        {
            try
            {
                await using (FSTR_DBContext fstr = new FSTR_DBContext())
                {
                    // TODO: Некорректная постановка задачи. Данные по пользователю хранятся в БД только в Json!
                    var routesList = await fstr.PerevalAddeds.Where(x => x.Id > 0).ToListAsync();

                    return Ok(routesList);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException?.Message);
            }
        }

        public async Task<IActionResult> GetRouteById(int id)
        {
            try
            {
                await using (FSTR_DBContext fstr = new FSTR_DBContext())
                {
                    var route = await fstr.PerevalAddeds.FirstOrDefaultAsync(x => x.Id == id);

                    return Ok(route);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException?.Message);
            }
        }

        public async Task<IActionResult> GetRouteStatus(int id)
        {
            try
            {
                await using (FSTR_DBContext fstr = new FSTR_DBContext())
                {
                    var route = await fstr.PerevalAddeds.Where(x => x.Id == id).Select(x => x.Status).FirstOrDefaultAsync();

                    return Ok(route);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException?.Message);
            }
        }

        // TODO: Необходимо дополнительно передавать объект Pereval!
        public async Task<IActionResult> EditRoute(int id)
        {
            try
            {
                await using (FSTR_DBContext fstr = new FSTR_DBContext())
                {
                    var route = await fstr.PerevalAddeds.FirstOrDefaultAsync(x => x.Id == id);

                    if (route != null && route.Status == "new")
                    {
                        // Редактируем запись
                    }

                    return Ok(route);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException?.Message);
            }
        }
    }
}
