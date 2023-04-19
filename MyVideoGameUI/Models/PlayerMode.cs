using System.ComponentModel.DataAnnotations;

namespace MyVideoGameUI.Models
{
    public class PlayerMode
    {

        [Key]
        public int PlayerModeId { get; set; }
        [Required]
        public string PlayerModeName { get; set; } = string.Empty;

    }
}
