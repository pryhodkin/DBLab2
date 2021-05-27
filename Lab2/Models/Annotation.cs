using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Lab2.Models
{
    public partial class Annotation
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public int SongId { get; set; }
        [Required]
        public string Lines { get; set; }
        [Required]
        public string Text { get; set; }

        public virtual User Author { get; set; }
        public virtual Song Song { get; set; }
    }
}
