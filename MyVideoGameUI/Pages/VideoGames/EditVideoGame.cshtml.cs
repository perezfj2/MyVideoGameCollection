using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection.Metadata;

namespace MyVideoGameUI.Pages.VideoGames
{
    public class EditVideoGameModel : PageModel
    {
        [BindProperty]
        public VideoGameDetail ExistingVideoGameDetail { get; set; } = new VideoGameDetail();
        [BindProperty]
        public VideoGame ExistingVideoGame { get; set; } = new VideoGame();
        [BindProperty]
        public List<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> Languages { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> PlayerModes { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> ContentRatings { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> GenreList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<int> GenresSelected { get; set; }
        [BindProperty]
        public List<SelectListItem> ConsoleList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<int> ConsolesSelected { get; set; }
        public void OnGet(int id)

        {
            PopulateVideoGameInfo(id);
            PopulatePublisherList();
            PopulateLanguageList();
            PopulatePlayerModeList();
            PopulateContentRatingList();
            PopulateGenreList();
            PopulateConsoleList();

            

        }

        public IActionResult OnPost(int id) //when user post the data, we grab it and use it
        {


            //if (ModelState.IsValid) //make sure the altered data is valid
            //
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {

                    //Step 2 - SQL statement
                    string sql = "UPDATE VideoGame SET GameTitle = @gameTitle, GameRating =@gameRating, LanguageId = @languageId,"+
                        " PublisherId = @publisherId, ReleaseDate = @releaseDate, PlayerModeId = @playerModeId, "+
                        "ContentRatingId = @contentRatingId, GameImageURL = @gameImageURL, GameSummary = @gameSummary WHERE GameId = @gameId";

                    //step 3 - SQL command object
                    //string sqlPublisherId = "SELECT @@Identity";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@gameTitle", ExistingVideoGame.GameTitle);
                    cmd.Parameters.AddWithValue("@gameRating", ExistingVideoGame.GameRating);
                    cmd.Parameters.AddWithValue("@languageId", ExistingVideoGame.LanguageId);
                    cmd.Parameters.AddWithValue("@publisherId", ExistingVideoGame.PublisherId);
                    cmd.Parameters.AddWithValue("@releaseDate", ExistingVideoGame.ReleaseDate);
                    cmd.Parameters.AddWithValue("@playerModeId", ExistingVideoGame.PlayerModeId);
                    cmd.Parameters.AddWithValue("@contentRatingId", ExistingVideoGame.ContentRatingId);
                    cmd.Parameters.AddWithValue("@gameImageURL", ExistingVideoGame.GameImageURL);
                    cmd.Parameters.AddWithValue("@gameSummary", ExistingVideoGame.GameSummary);
                    cmd.Parameters.AddWithValue("@gameId", id);
                    //step 4 - open connection
                    conn.Open();
                    //step 5 - execute
                    cmd.ExecuteNonQuery();

                //delete all records from VideoGameGenre table

                    sql = "DELETE From VideoGameGenre WHERE GameId = @gameId";
                    
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@gameId", id);
                    
                    int r = cmd.ExecuteNonQuery();

                    string sqlGenre = "INSERT INTO VideoGameGenre(GameId, GenreId) VALUES (@gameId, @genreId)";
                    cmd.CommandText = sqlGenre;
                    cmd.Parameters.Clear();
                    for (int i = 0; i < GenresSelected.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue("@gameId", id);
                        cmd.Parameters.AddWithValue("@genreId", GenresSelected[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    sql = "DELETE From VideoGameConsole WHERE GameId = @gameId";

                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@gameId", id);

                    r = cmd.ExecuteNonQuery();

                    string sqlConsole = "INSERT INTO VideoGameConsole(GameId, ConsoleId) VALUES (@gameId, @consoleId)";
                    cmd.CommandText = sqlConsole;
                    cmd.Parameters.Clear();
                    for (int i = 0; i < ConsolesSelected.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue("@gameId", id);
                        cmd.Parameters.AddWithValue("@consoleId", ConsolesSelected[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }


                return RedirectToPage("Index");


                    
                }



                //step 6 - close the sql connection

            //}//if valid

            //else //not valid, return to the same page.
            //{
             //   return Page();
            //}


        }

        private void PopulateVideoGameInfo(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlInfo = "SELECT * FROM VideoGame WHERE GameId=@gameId";
                SqlCommand cmd = new SqlCommand(sqlInfo, conn);
                cmd.Parameters.AddWithValue("@gameId", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read(); //read the first record
                    ExistingVideoGame.GameId = id;
                    ExistingVideoGame.GameTitle = reader["GameTitle"].ToString();
                    ExistingVideoGame.GameImageURL = reader["GameImageURL"].ToString();
                    ExistingVideoGame.GameSummary = reader["GameSummary"].ToString();
                    ExistingVideoGame.PublisherId = int.Parse(reader["PublisherId"].ToString());
                    ExistingVideoGame.GameRating = decimal.Parse(reader["GameRating"].ToString());
                    ExistingVideoGame.LanguageId = int.Parse(reader["LanguageId"].ToString());
                    ExistingVideoGame.ReleaseDate = (DateTime)reader["ReleaseDate"];
                    ExistingVideoGame.PlayerModeId = int.Parse(reader["PlayerModeId"].ToString());
                    ExistingVideoGame.ContentRatingId = int.Parse(reader["ContentRatingId"].ToString());
                }
            }
            
        }

        private void PopulateGenreList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT GenreId, GenreName FROM Genre Order By GenreName";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Text = reader["GenreName"].ToString();
                        item.Value = reader["GenreId"].ToString();
                        GenreList.Add(item);
                    }
                }
            }

            // retrieve a list of BookGenre records {bookId, genreId} from database
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlGenres = "SELECT GenreId FROM VideoGameGenre WHERE GameId=@gameId";
                SqlCommand cmdGenres = new SqlCommand(sqlGenres, conn);
                cmdGenres.Parameters.AddWithValue("@gameId", ExistingVideoGame.GameId);
                conn.Open();
                SqlDataReader readerGenre = cmdGenres.ExecuteReader();
                if (readerGenre.HasRows)
                {
                    while (readerGenre.Read())
                    {
                        foreach (SelectListItem i in GenreList)
                        {
                            if (i.Value == readerGenre["GenreId"].ToString())
                            {
                                i.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void PopulateConsoleList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlConsole = "SELECT ConsoleId, ConsoleName FROM Console Order By ConsoleName";
                SqlCommand cmd = new SqlCommand(sqlConsole, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var console = new SelectListItem();
                        console.Value = reader["ConsoleId"].ToString();
                        console.Text = reader["ConsoleName"].ToString();
                        ConsoleList.Add(console);
                    }
                }
            }
            //retrieve a list of VideoGameGenre records {gameId, genreId} from database.
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlConsoles = "SELECT ConsoleId FROM VideoGameConsole WHERE GameId = @gameId";
                SqlCommand cmdConsoles = new SqlCommand(sqlConsoles, conn);
                cmdConsoles.Parameters.AddWithValue("@gameId", ExistingVideoGame.GameId);
                conn.Open();
                SqlDataReader readerConsole = cmdConsoles.ExecuteReader();
                if (readerConsole.HasRows)
                {
                    while (readerConsole.Read())
                    {
                        foreach (SelectListItem i in ConsoleList)
                        {
                            if (i.Value == readerConsole["ConsoleId"].ToString())
                            {
                                i.Selected = true;
                                break;

                            }
                        }
                    }
                }
            }
        }

        private void PopulateLanguageList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlLanguage = "SELECT LanguageId, LanguageName FROM Language Order By LanguageName";
                SqlCommand cmd = new SqlCommand(sqlLanguage, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var language = new SelectListItem();
                        language.Value = reader["LanguageId"].ToString();
                        language.Text = reader["LanguageName"].ToString();
                        Languages.Add(language);
                    }
                }
            }
        }

        private void PopulateContentRatingList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlContentRating = "SELECT ContentRatingId, ContentRatingName FROM ContentRating Order By ContentRatingName";
                SqlCommand cmd = new SqlCommand(sqlContentRating, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var contentRating = new SelectListItem();
                        contentRating.Value = reader["ContentRatingId"].ToString();
                        contentRating.Text = reader["ContentRatingName"].ToString();
                        ContentRatings.Add(contentRating);
                    }
                }
            }
        }

        private void PopulatePlayerModeList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlPlayerMode = "SELECT PlayerModeId, PlayerModeName FROM PlayerMode Order By PlayerModeName";
                SqlCommand cmd = new SqlCommand(sqlPlayerMode, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var playerMode = new SelectListItem();
                        playerMode.Value = reader["PlayerModeId"].ToString();
                        playerMode.Text = reader["PlayerModeName"].ToString();
                        PlayerModes.Add(playerMode);
                    }
                }
            }
        }

        private void PopulatePublisherList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlPublisher = "SELECT PublisherId, PublisherName FROM Publisher Order By PublisherName";
                SqlCommand cmd = new SqlCommand(sqlPublisher, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var publisher = new SelectListItem();
                        publisher.Value = reader["PublisherId"].ToString();
                        publisher.Text = reader["PublisherName"].ToString();
                        Publishers.Add(publisher);
                    }
                }
            }
        }
    }
}
