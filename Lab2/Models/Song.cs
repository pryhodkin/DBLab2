using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Lab2.Models
{
    public partial class Song
    {
        public Song()
        {
            Annotations = new HashSet<Annotation>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int MainArtistId { get; set; }
        public int? SecondaryArtistId { get; set; }
        public int? AlbumId { get; set; }
        [Required]
        public string Lyrics { get; set; }
        public int? LabelId { get; set; }

        public virtual Album Album { get; set; }
        public virtual Label Label { get; set; }
        public virtual Artist MainArtist { get; set; }
        public virtual Artist SecondaryArtist { get; set; }
        public virtual ICollection<Annotation> Annotations { get; set; }
    }
}
