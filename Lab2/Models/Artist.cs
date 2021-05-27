using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Lab2.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
            SongMainArtists = new HashSet<Song>();
            SongSecondaryArtists = new HashSet<Song>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string NickName { get; set; }
        public int? LabelId { get; set; }

        public virtual Label Label { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Song> SongMainArtists { get; set; }
        public virtual ICollection<Song> SongSecondaryArtists { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
