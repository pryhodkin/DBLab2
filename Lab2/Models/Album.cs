using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Lab2.Models
{
    public partial class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
