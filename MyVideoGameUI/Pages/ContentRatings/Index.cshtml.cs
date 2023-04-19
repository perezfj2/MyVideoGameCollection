using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyVideoGameUI.Pages.ContentRatings
{
    public class IndexModel : PageModel
    {
        public List<ContentRating> ContentRatings { get; set; } = new List<ContentRating>();
        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM ContentRating";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentRating contentRating = new ContentRating();
                        contentRating.ContentRatingId = int.Parse(reader["ContentRatingId"].ToString());
                        contentRating.ContentRatingName = reader["ContentRatingName"].ToString();
                        ContentRatings.Add(contentRating);
                    }
                }

            }
        }

        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM ContentRating WHERE ContentRatingId = @contentRatingId"; //delete sql statement
                SqlCommand cmd = new SqlCommand(sql, conn); //make command
                cmd.Parameters.AddWithValue("@contentRatingId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");

            }
        }
    }
}
