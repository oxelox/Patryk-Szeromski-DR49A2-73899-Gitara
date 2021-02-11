using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gitara.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Imię")]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        [MinLength(5, ErrorMessage = "{0} musi mieć conajmniej {1} znaków.")]
        public string Haslo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Haslo")]
        [DisplayName("Powtórz hasło")]
        public string PowtorzenieHasla { get; set; }
    }
}