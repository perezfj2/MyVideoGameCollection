using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.Sql;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.Publishers
{
    public class EditPublisherModel : PageModel
    {
        [BindProperty]
        public Publisher ExistingPublisher { get; set; } = new Publisher();
        public void OnGet(int id)
        {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString())) //set up connection
                {
                    
                    string sql = "SELECT * FROM Publisher WHERE PublisherId = @publisherId"; //sql query
                    //step 3 - SQL command object
                    SqlCommand cmd = new SqlCommand(sql, conn); //set up a command
                    cmd.Parameters.AddWithValue("@publisherId", id); //pass in parameter value

                    //step 4 - open connection
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(); //put it in the reader
                    if (reader.HasRows) //check if there is anything in the reader
                    {
                        reader.Read(); //read the first record
                        ExistingPublisher.PublisherName = reader["PublisherName"].ToString();   //populate existing object
                        ExistingPublisher.PublisherBio = reader["PublisherBio"].ToString();
                        ExistingPublisher.PublisherLogoURL = reader["PublisherLogoURL"].ToString();
                        ExistingPublisher.PublisherWebsite = reader["PublisherWebsite"].ToString();
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
                    string sql = "UPDATE Publisher SET PublisherName = @publisherName, PublisherBio = @publisherBio, PublisherLogoURL = @publisherLogoURL," +
                        "PublisherWebsite = @publisherWebsite WHERE PublisherId = @publisherId";
                    //step 3 - SQL command object
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@publisherName", ExistingPublisher.PublisherName);
                    cmd.Parameters.AddWithValue("@publisherBio", ExistingPublisher.PublisherBio);
                    cmd.Parameters.AddWithValue("@publisherLogoURL", ExistingPublisher.PublisherLogoURL);
                    cmd.Parameters.AddWithValue("@publisherWebsite", ExistingPublisher.PublisherWebsite);
                    cmd.Parameters.AddWithValue("@publisherId", id);
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
