﻿using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Data
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string Country { get; set; } = null!;

        [Required] 
        public string AuthorId { get; set; } = null!;

        public virtual User Author { get; set; } = null!;
    }
}