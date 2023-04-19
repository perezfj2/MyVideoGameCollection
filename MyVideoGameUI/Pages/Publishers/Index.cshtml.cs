using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyVideoGameUI.Models;

namespace MyVideoGameUI.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Publisher> PublisherList { get; set; } = new List<Publisher>();
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
                string sql = "SELECT * FROM Publisher ORDER BY PublisherName";
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
                        Publisher publisher = new Publisher();
                        publisher.PublisherName = reader["PublisherName"].ToString();
                        publisher.PublisherBio = reader["PublisherBio"].ToString();
                        publisher.PublisherLogoURL = reader["PublisherLogoURL"].ToString();
                        publisher.PublisherWebsite = reader["PublisherWebsite"].ToString();
                        publisher.PublisherId = int.Parse(reader["PublisherId"].ToString());
                        PublisherList.Add(publisher);
                    }
                }

            }


        }

        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM Publisher WHERE PublisherId = @publisherId"; //delete sql statement
                SqlCommand cmd = new SqlCommand(sql, conn); //make command
                cmd.Parameters.AddWithValue("@publisherId", id); 
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");

            }
        }
    }
}
