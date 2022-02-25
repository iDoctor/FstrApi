using System.ComponentModel.DataAnnotations;

namespace FstrApi.Structures
{
    public class Pereval
    {
        [Required]
        public string id { get; set; }
        public string beautyTitle { get; set; }
        [Required]
        public string title { get; set; }
        public string other_titles { get; set; }
        public string connect { get; set; }
        [Required]
        public string add_time { get; set; }
        public User user { get; set; }
        public Coords coords { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public Level level { get; set; }
        [Required]
        public List<Image> images { get; set; }
    }

    public class Image
    {
        [Required]
        public string url { get; set; }
        [Required]
        public string title { get; set; }
    }

    public class Level
    {
        public string winter { get; set; }
        public string summer { get; set; }
        public string autumn { get; set; }
        public string spring { get; set; }
    }

    public class Coords
    {
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        [Required]
        public string height { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string fam { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string otc { get; set; }
    }
}
