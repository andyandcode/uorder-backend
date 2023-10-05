﻿using Data.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Utilities.Common;

namespace Models.Dishes
{
    public class DishCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.Menu);

        [Required]
        public string Name { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int CompletionTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int QtyPerDate { get; set; }

        [Required]
        public DishType Type { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt => DateTime.Now;
    }
}