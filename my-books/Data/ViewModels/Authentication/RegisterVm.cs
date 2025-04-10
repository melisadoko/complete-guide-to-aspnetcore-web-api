﻿using System.ComponentModel.DataAnnotations;

namespace my_books.Data.ViewModels.Authentication
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

    }
}
