﻿using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Data
{
    public class Person
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime? Born { get; set; }

        [MaxLength(6)]
        public string? Gender { get; set; }

        public List<Movie> Movies { get; set; } = new();
        public List<Cast> Casts { get; set; } = new();

    }
}
