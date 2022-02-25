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
        public async Task<List<int>> LoadImages(List<Image> images)
        {
            List<int> result = new List<int>();

            foreach(Image image in images)
            {
                Uri? uriResult;
                bool isLinkOk = Uri.TryCreate(image.url, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (isLinkOk)
                {
                    byte[]? imageBytes = null;
                    try
                    {
                        using (HttpClient client = new HttpClient())
                            imageBytes = await client.GetByteArrayAsync(image.url);
                    }
                    catch (Exception)
                    {
                    }

                    if (imageBytes != null)
                    {
                        try
                        {
                            await using (FSTR_DBContext fstr = new FSTR_DBContext())
                            {
                                var perevalImage = new PerevalImage
                                {
                                    DateAdded = DateTime.Now,
                                    Img = imageBytes
                                };

                                fstr.PerevalImages.Add(perevalImage);

                                await fstr.SaveChangesAsync();

                                result.Add(perevalImage.Id);
                            }
                        }
                        catch (Exception)
                        {                            
                        }
                    }                        
                }
            }

            return result;
        }
    }
}
