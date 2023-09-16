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
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        public string OldPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        [Compare("NewPassword", ErrorMessage = "New password and it`s confirmation does not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
