using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gitara.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Haslo { get; set; }
    }
}