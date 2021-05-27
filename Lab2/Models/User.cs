using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Lab2.Models
{
    public partial class User
    {
        public User()
        {
            Annotations = new HashSet<Annotation>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string NickName { get; set; }

        public virtual ICollection<Annotation> Annotations { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
