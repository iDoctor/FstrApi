using System;
using System.Collections.Generic;

namespace FstrApi.Models
{
    public partial class PerevalAdded
    {
        public int Id { get; set; }
        public DateTime? DateAdded { get; set; }
        public string? RawData { get; set; }
        public string? Images { get; set; }
        public string? Status { get; set; }
    }
}
