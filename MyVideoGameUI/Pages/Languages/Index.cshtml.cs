using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyVideoGameUI.Pages.Languages
{
    public class IndexModel : PageModel
    {
        public List<Language> Languages { get; set; } = new List<Language>();
        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Language";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Language language = new Language();
                        language.LanguageId = int.Parse(reader["LanguageId"].ToString());
                        language.LanguageName = reader["LanguageName"].ToString();
                        Languages.Add(language);
                    }
                }

            }
        }

        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM Language WHERE LanguageId = @LanguageId"; //delete sql statement
                SqlCommand cmd = new SqlCommand(sql, conn); //make command
                cmd.Parameters.AddWithValue("@LanguageId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");

            }
        }
    }
}
