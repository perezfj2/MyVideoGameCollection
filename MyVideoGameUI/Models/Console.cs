using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyVideoGameUI.Models
{
    public class Console
    {

        [Key]
        public int ConsoleId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Console Name:")]
        public String ConsoleName { get; set; } = string.Empty;
        [DisplayName("Console Image URL:")]
        public String ConsoleImageURL { get; set; } = string.Empty;

    }
}
