using Microsoft.AspNetCore.Mvc;
using FstrApi.Structures;
using FstrApi.Models;
using System.Net;
using Newtonsoft.Json;

namespace FstrApi.Controllers
{
    public class ImagesController : Controller
    {
        /// <summary>
        /// Загрузка изображений
        /// </summary>
        /// <param name="images">Список изображений</param>
        public async Task<LoadedImage> LoadImages(List<Image> images)
        {
            LoadedImage loadedImagesList = new LoadedImage()
            {
                Sedlo = new List<int>(),
                Nord = new List<int>(),
                West = new List<int>(),
                South = new List<int>(),
                East = new List<int>()
            };

            foreach(Image image in images)
            {
                if (image.title != "Sedlo" && image.title != "Nord" && image.title != "West" && image.title != "South" && image.title != "East")
                    break;

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

                                switch (image.title)
                                {
                                    case "Sedlo":
                                        loadedImagesList.Sedlo.Add(perevalImage.Id);
                                        break;
                                    case "Nord":
                                        loadedImagesList.Nord.Add(perevalImage.Id);
                                        break;
                                    case "West":
                                        loadedImagesList.West.Add(perevalImage.Id);
                                        break;
                                    case "South":
                                        loadedImagesList.South.Add(perevalImage.Id);
                                        break;
                                    case "East":
                                        loadedImagesList.East.Add(perevalImage.Id);
                                        break;
                                }
                            }
                        }
                        catch (Exception)
                        {                            
                        }
                    }                        
                }
            }

            return loadedImagesList;
        }
    }
}
