﻿using MovieAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Entities
{
    public class PersonDTO
    {
        public required string Name { get; set; }
        public DateOnly Born { get; set; }

        [MaxLength(6)]
        public string? Gender { get; set; }
        public List<CastDTO> Casts { get; set; } = [];

    }
}
