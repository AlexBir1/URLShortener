using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Models
{
    public class UpdateAccountModel
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } 

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        public string OldPassword { get; set; } 

        public string NewPassword { get; set; } 

        [Compare("NewPassword", ErrorMessage = "New password and it`s confirmation does not match.")]
        public string ConfirmNewPassword { get; set; } 
    }
}
