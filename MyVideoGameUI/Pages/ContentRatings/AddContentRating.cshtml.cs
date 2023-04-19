using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyVideoGameUI.Pages.ContentRatings
{
    public class AddContentRatingModel : PageModel
    {
        [BindProperty]
        public ContentRating NewContentRating { get; set; } = new ContentRating();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO ContentRating(ContentRatingName) VALUES (@contentRatingName)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@contentRatingName", NewContentRating.ContentRatingName);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");
            }
        }
    }
}
