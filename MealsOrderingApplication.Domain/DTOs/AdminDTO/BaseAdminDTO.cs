﻿using MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.AdminDTO
{
    public class BaseAdminDTO : BaseApplicationUserDTO
    {
        [Required(ErrorMessage = "The FirstName field is required.")]
        [StringLength(100, ErrorMessage = "The FirstName field must be less than 100 characters.")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "The LastName field is required.")]
        [StringLength(100, ErrorMessage = "The LastName field must be less than 100 characters.")]
        public string LastName { get; set; } = default!;


        [Required(ErrorMessage = "The Email field is required.")]
        [StringLength(256, ErrorMessage = "The Email field must be less than 256 characters.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "The Username field is required.")]
        [StringLength(256, ErrorMessage = "The Username field must be less than 256 characters.")]
        public string Username { get; set; } = default!;
    }
}