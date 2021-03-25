using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace HCMDataAccess.Models
{
    public class LoginRequest
    {
        [JsonProperty("userName")]
        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
