using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.PlayerModes
{
    public class IndexModel : PageModel
    {
        public List<PlayerMode> PlayerModes { get; set; } = new List<PlayerMode>();
        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM PlayerMode";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PlayerMode playerMode = new PlayerMode();
                        playerMode.PlayerModeId = int.Parse(reader["PlayerModeId"].ToString());
                        playerMode.PlayerModeName = reader["PlayerModeName"].ToString();
                        PlayerModes.Add(playerMode);
                    }
                }

            }


        }

        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM PlayerMode WHERE PlayerModeId = @PlayerModeId"; //delete sql statement
                SqlCommand cmd = new SqlCommand(sql, conn); //make command
                cmd.Parameters.AddWithValue("@PlayerModeId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");

            }
        }
    }
}
