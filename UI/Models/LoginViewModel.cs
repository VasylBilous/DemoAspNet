using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Логін не може бути порожнім")]
        public string Login { get; set; }
        [Required(ErrorMessage = "ВВедіть пароль")]
        public string Password { get; set; }
    }
}