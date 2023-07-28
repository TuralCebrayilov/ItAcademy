using System.ComponentModel.DataAnnotations;

namespace ItAcademy.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        public string Username { get; set; }
        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string CheckPassword { get; set; }
       
    }
}
