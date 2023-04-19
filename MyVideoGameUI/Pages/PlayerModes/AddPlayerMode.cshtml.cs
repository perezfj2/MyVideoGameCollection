using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyVideoGameUI.Pages.PlayerModes
{
    public class AddPlayerModeModel : PageModel
    {
        [BindProperty]
        public PlayerMode NewPlayerMode { get; set; } = new PlayerMode();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO PlayerMode(PlayerModeName) VALUES (@playerModeName)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@playerModeName", NewPlayerMode.PlayerModeName);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");
            }
        }
    }
}
