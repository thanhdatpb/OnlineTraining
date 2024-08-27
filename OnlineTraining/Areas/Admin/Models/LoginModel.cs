using System.ComponentModel.DataAnnotations;

namespace OnlineTraining.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mởi bạn nhập UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mởi bạn nhập PassWord")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}