﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class TeamRequestModel : BaseRequestModel
    {
        [Url]
        [DisplayName("Logo")]
        public string? LogoUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int Points { get; set; }

        [Required]
        [DisplayName("League")]
        public Guid LeagueId { get; set; }
    }
}