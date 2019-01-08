using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NB.MOBILEPO.BAL.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Password { get; set; }
    }
}
