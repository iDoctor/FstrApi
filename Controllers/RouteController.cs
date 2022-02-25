using FstrApi.Models;
using FstrApi.Structures;
using Microsoft.AspNetCore.Mvc;
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
    }
}
