﻿using System.ComponentModel.DataAnnotations;
namespace TMS_Api.DTOs
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "User Role is required.")]
        public string? Role { get; set; }
        public string? CreatedUser { get; set; }
        public string? UpdatedUser { get; set; }    
    }
}
