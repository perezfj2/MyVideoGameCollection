using System.ComponentModel.DataAnnotations;

namespace MyVideoGameUI.Models
{
    public class ContentRating
    {

        [Key]
        public int ContentRatingId { get; set; }
        [Required]
        public string ContentRatingName { get; set; } = string.Empty;

    }
}
