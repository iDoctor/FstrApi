using System;
using System.Collections.Generic;

namespace FstrApi.Models
{
    public partial class PerevalArea
    {
        public long Id { get; set; }
        public long IdParent { get; set; }
        public string? Title { get; set; }
    }
}
