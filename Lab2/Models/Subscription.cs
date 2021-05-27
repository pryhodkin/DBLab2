using System;
using System.Collections.Generic;

#nullable disable

namespace Lab2.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int UserId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual User User { get; set; }
    }
}
