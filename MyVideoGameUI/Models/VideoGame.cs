using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyVideoGameUI.Models
{
    public class VideoGame
    {

        [Key]
        public int GameId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Game Title:")]
        public String GameTitle { get; set; } = string.Empty;

        [DisplayName("Game Image URL:")]
        public String GameImageURL { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Game Descrpition:")]
        public String GameSummary{ get; set; } = string.Empty;
        public int LanguageId { get; set; }
        [Key]
        public int PublisherId { get; set; }
        [Key]
        public int PlayerModeId { get; set; }
        [Key]
        public int ContentRatingId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public decimal GameRating { get; set; } = 0;
        [Required(ErrorMessage = "This field is required!")]
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;




    }
}

