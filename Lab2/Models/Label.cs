using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Lab2.Models
{
    public partial class Label
    {
        public Label()
        {
            Artists = new HashSet<Artist>();
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
