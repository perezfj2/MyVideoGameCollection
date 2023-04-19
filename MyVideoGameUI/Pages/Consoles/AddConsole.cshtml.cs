using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.Consoles
{
    public class AddConsoleModel : PageModel
    {
        [BindProperty]
        public Models.Console NewConsole { get; set; } = new Models.Console();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            /*
            * 1. Create a SQL Connection object.
            * 2. Construct a SQL statement.
            * 3. Create a SQL command object.
            * 4. Open the SQL connection.
            * 5. Execute the SQL command.
            * 6. Close the SQL connection.
            * */

            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    //Step 1 - Creating a connection

                    //Step 2 - SQL statement
                    string sql = "INSERT INTO Console(ConsoleName, ConsoleImageURL)" +
                        "VALUES (@consoleName, @consoleImageURL)";
                    //step 3 - SQL command object
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@consoleName", NewConsole.ConsoleName);
                    cmd.Parameters.AddWithValue("@consoleImageURL", NewConsole.ConsoleImageURL);

                    //step 4 - open connection
                    conn.Open();
                    //step 5 - execute
                    cmd.ExecuteNonQuery();
                }

                return RedirectToPage("Index");

                //step 6 - close the sql connection

            }//if valid

            else
            {
                return Page();
            }


        }
    }
}
