using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.Sql;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.Consoles
{
    public class EditConsoleModel : PageModel
    {
        [BindProperty]
        public Models.Console ExistingConsole { get; set; } = new Models.Console();
        public void OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString())) //set up connection
            {

                string sql = "SELECT * FROM Console WHERE ConsoleId = @consoleId"; //sql query
                                                                                         //step 3 - SQL command object
                SqlCommand cmd = new SqlCommand(sql, conn); //set up a command
                cmd.Parameters.AddWithValue("@consoleId", id); //pass in parameter value

                //step 4 - open connection
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(); //put it in the reader
                if (reader.HasRows) //check if there is anything in the reader
                {
                    reader.Read(); //read the first record
  
                    ExistingConsole.ConsoleName = reader["ConsoleName"].ToString(); //populate existing object
                    ExistingConsole.ConsoleImageURL = reader["ConsoleImageURL"].ToString();
        
                }

            }

        }

        public IActionResult OnPost(int id) //when user post the data, we grab it and use it
        {

            if (ModelState.IsValid) //make sure the altered data is valid
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {

                    //Step 2 - SQL statement
                    string sql = "UPDATE Console SET ConsoleName = @consoleName, ConsoleImageURL = @consoleImageURL WHERE ConsoleId = @consoleId";
                    //step 3 - SQL command object
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@consoleName", ExistingConsole.ConsoleName);
                    cmd.Parameters.AddWithValue("@consoleImageURL", ExistingConsole.ConsoleImageURL);
                    cmd.Parameters.AddWithValue("@consoleId", id);
                    //step 4 - open connection
                    conn.Open();
                    //step 5 - execute
                    cmd.ExecuteNonQuery();
                    return RedirectToPage("Index"); //once data has been modified, return user back to the Publisher Index Screen.
                }



                //step 6 - close the sql connection

            }//if valid

            else //not valid, return to the same page.
            {
                return Page();
            }


        }
    }
}
