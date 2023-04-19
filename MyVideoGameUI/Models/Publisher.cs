using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyVideoGameUI.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Publisher Name:")]
        public String PublisherName { get; set; } = string.Empty;

        [DisplayName("Publisher Info:")]
        public String PublisherBio { get; set; } = string.Empty;

        [DisplayName("Publisher Logo URL:")]
        public String PublisherLogoURL { get; set; } = string.Empty;
        [DisplayName("Publisher Website:")]
        public String PublisherWebsite { get; set; } = string.Empty;

    }
}
