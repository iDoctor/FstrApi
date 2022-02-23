using System;
using System.Collections.Generic;

namespace FstrApi.Models
{
    public partial class PerevalImage
    {
        public int Id { get; set; }
        public DateTime? DateAdded { get; set; }
        public byte[] Img { get; set; } = null!;
    }
}
