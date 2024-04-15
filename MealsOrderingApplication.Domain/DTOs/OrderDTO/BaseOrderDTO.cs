﻿using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.OrderDTO
{
    public class BaseOrderDTO : BaseDTO
    {
        // Order Table
        public string? Description { get; set; }

        [Required(ErrorMessage = "The CustomerId field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be greater than 0.")]
        public string CustomerId { get; set; } = default!;

        [Required(ErrorMessage = "The Quantities List field is required.")]
        public List<int> Quantities { get; set; } = default!;

        [Required(ErrorMessage = "The Products List field is required.")]
        public List<int> ProductsId { get; set; } = default!;
    }
}
