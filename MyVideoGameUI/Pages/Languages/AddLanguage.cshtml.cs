using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyVideoGameUI.Pages.Languages
{
    public class AddLanguageModel : PageModel
    {
        [BindProperty]
        public Language NewLanguage { get; set; } = new Language();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO Language(LanguageName) VALUES (@languageName)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@languageName", NewLanguage.LanguageName);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");
            }
        }
    }
}
