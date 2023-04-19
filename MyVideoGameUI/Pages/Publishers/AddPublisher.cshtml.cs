using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.Publishers
{
    public class AddPublisherModel : PageModel
    {
        [BindProperty]
        public Publisher NewPublisher { get; set; } = new Publisher();
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
                    string sql = "INSERT INTO Publisher(PublisherName, PublisherBio, PublisherLogoURL, PublisherWebsite)" +
                        "VALUES (@publisherName, @publisherBio, @publisherLogoURL, @publisherWebsite)";
                    //step 3 - SQL command object
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@publisherName", NewPublisher.PublisherName);
                    cmd.Parameters.AddWithValue("@publisherBio", NewPublisher.PublisherBio);
                    cmd.Parameters.AddWithValue("@publisherLogoURL", NewPublisher.PublisherLogoURL);
                    cmd.Parameters.AddWithValue("@publisherWebsite", NewPublisher.PublisherWebsite);

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
