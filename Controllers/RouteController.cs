using FstrApi.Models;
using FstrApi.Structures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FstrApi.Controllers
{
    public class RouteController : Controller
    {
        public async Task<IActionResult> SaveNewRoute(Pereval pereval, LoadedImage images)
        {
            try
            {
                DateTime addedDt;
                addedDt = DateTime.TryParse(pereval.add_time, out addedDt) ? addedDt : DateTime.Now;

                await using (FSTR_DBContext fstr = new FSTR_DBContext())
                {
                    var newRoute = new PerevalAdded
                    {
                        DateAdded = addedDt,
                        RawData = JsonConvert.SerializeObject(pereval, Formatting.Indented),
                        Images = JsonConvert.SerializeObject(images, Formatting.Indented),
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
                    List<PerevalAdded> routesList = fstr.PerevalAddeds.FromSqlRaw($"SELECT * FROM pereval_added WHERE id > 0" +
                            (!string.IsNullOrEmpty(user.email) ? $" AND raw_data->'user'->>'email' = '{user.email}'" : string.Empty) +
                            (!string.IsNullOrEmpty(user.phone) ? $" AND raw_data->'user'->>'phone' = '{user.phone}'" : string.Empty) +
                            (!string.IsNullOrEmpty(user.fam) ? $" AND raw_data->'user'->>'fam' = '{user.fam}'" : string.Empty) +
                            (!string.IsNullOrEmpty(user.name) ? $" AND raw_data->'user'->>'name' = '{user.name}'" : string.Empty) +
                            (!string.IsNullOrEmpty(user.otc) ? $" AND raw_data->'user'->>'otc' = '{user.otc}'" : string.Empty)).ToList();

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

        // TODO: Необходимо дополнительно передавать объект Pereval! (какой формат??)
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
