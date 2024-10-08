﻿using System.ComponentModel.DataAnnotations;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class ResetPasswordVM
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Password and Confirmed password must be match")]
    public string ConfirmPassword { get; set; }

}
