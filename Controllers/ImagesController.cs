using Microsoft.AspNetCore.Mvc;
using FstrApi.Structures;
using FstrApi.Models;
using System.Net;

namespace FstrApi.Controllers
{
    public class ImagesController : Controller
    {
        /// <summary>
        /// Загрузка изображений
        /// </summary>
        /// <param name="images">Список изображений</param>
        public async void LoadImages(List<Image> images)
        {
            foreach(Image image in images)
            {
                Uri? uriResult;
                bool isLinkOk = Uri.TryCreate(image.url, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (isLinkOk)
                {
                    // Устаревший механизм
                    //WebClient client = new WebClient();
                    //byte[] data = client.DownloadData(image.url);

                    HttpClient ht = new HttpClient();
                    var imageBytes = await ht.GetByteArrayAsync(image.url);

                    using (FSTR_DBContext fstr = new FSTR_DBContext())
                    {
                        var v = fstr.PerevalAreas.Where(x => x.Id > 0).ToList();

                        fstr.PerevalImages.Add(
                            new PerevalImage
                            {
                                DateAdded = DateTime.Now,
                                Img = imageBytes
                            });

                        var v1 = fstr.SaveChanges();
                    }
                }
            }
        }
    }
}
