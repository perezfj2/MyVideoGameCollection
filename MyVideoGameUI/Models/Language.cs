using System.ComponentModel.DataAnnotations;

namespace MyVideoGameUI.Models
{
    public class Language
    {

        [Key]
        public int LanguageId { get; set; }
        [Required]
        public string LanguageName { get; set; } = string.Empty;
    }
}
