using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.Consoles
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Models.Console> ConsoleList { get; set; } = new List<Models.Console>();
        public void OnGet()
        {
            /*
             * 1. Create a SQL Connection object.
             * 2. Construct a SQL statement.
             * 3. Create a SQL command object.
             * 4. Open the SQL connection.
             * 5. Execute the SQL command.
             * 6. Close the SQL connection.
             * */
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                //Step 1 - Creating a connection

                //Step 2 - SQL statement
                string sql = "SELECT * FROM Console ORDER BY ConsoleName";
                //step 3 - SQL command object
                SqlCommand cmd = new SqlCommand(sql, conn);
                //step 4 - open connection
                conn.Open();
                //step 5 - execute
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Models.Console console = new Models.Console();
                        console.ConsoleId = int.Parse(reader["ConsoleId"].ToString());
                        console.ConsoleName = reader["ConsoleName"].ToString();
                        console.ConsoleImageURL = reader["ConsoleImageURL"].ToString();
                        ConsoleList.Add(console);
                    }
                }

            }


        }

        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM Console WHERE ConsoleId = @consoleId"; //delete sql statement
                SqlCommand cmd = new SqlCommand(sql, conn); //make command
                cmd.Parameters.AddWithValue("@consoleId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");

            }
        }
    }
}
