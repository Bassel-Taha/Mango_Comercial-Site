﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Mango.Web.Models
{
    public class LoginRequestDTO
    {
        public string UserName { get; set; }
        [PasswordPropertyText]
        [StringLength(15, ErrorMessage = "password must be between 8 to 15 charachters", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
