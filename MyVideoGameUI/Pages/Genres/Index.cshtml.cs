using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.Genres
{
    public class IndexModel : PageModel
    {
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Genre";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre();
                        genre.GenreId = int.Parse(reader["GenreId"].ToString());
                        genre.GenreName = reader["GenreName"].ToString();
                        Genres.Add(genre);
                    }
                }

            }
        }

        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM Genre WHERE GenreId = @GenreId"; //delete sql statement
                SqlCommand cmd = new SqlCommand(sql, conn); //make command
                cmd.Parameters.AddWithValue("@GenreId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");

            }
        }
    }

}
