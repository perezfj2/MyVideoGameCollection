using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace MyVideoGameUI.Pages.VideoGames
{
    public class AddVideoGameModel : PageModel
    {
        [BindProperty]
        public VideoGame NewVideoGame { get; set; } = new VideoGame();

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
        public void OnGet()
        {
            PopulatePublisherList();
            PopulateLanguageList();
            PopulatePlayerModeList();
            PopulateContentRatingList();
            PopulateGenreList();
            PopulateConsoleList();

        }

        private void PopulateGenreList()
        {
            //If valid
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlGenre = "SELECT GenreId, GenreName FROM Genre Order By GenreName";
                SqlCommand cmd = new SqlCommand(sqlGenre, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var genre = new SelectListItem();
                        genre.Value = reader["GenreId"].ToString();
                        genre.Text = reader["GenreName"].ToString();
                        GenreList.Add(genre);
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
                    string sql = "INSERT INTO VideoGame(GameTitle, GameRating, LanguageId, PublisherId, ReleaseDate, PlayerModeId, ContentRatingId, GameImageURL, GameSummary)" +
                        "VALUES (@gameTitle, @gameRating, @languageId, @publisherId, @releaseDate, @playerModeId, @contentRatingId, @gameImageURL, @gameSummary)";
                    //step 3 - SQL command object
                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@gameTitle", NewVideoGame.GameTitle);
                    cmd.Parameters.AddWithValue("@gameRating", NewVideoGame.GameRating);
                    cmd.Parameters.AddWithValue("@languageId", NewVideoGame.LanguageId);
                    cmd.Parameters.AddWithValue("@publisherId", NewVideoGame.PublisherId);
                    cmd.Parameters.AddWithValue("@releaseDate", NewVideoGame.ReleaseDate);
                    cmd.Parameters.AddWithValue("@playerModeId", NewVideoGame.PlayerModeId);
                    cmd.Parameters.AddWithValue("@contentRatingId", NewVideoGame.ContentRatingId);
                    cmd.Parameters.AddWithValue("@gameImageURL", NewVideoGame.GameImageURL);
                    cmd.Parameters.AddWithValue("@gameSummary", NewVideoGame.GameSummary);

                    //step 4 - open connection
                    conn.Open();
                    //step 5 - execute
                    cmd.ExecuteNonQuery();
                    string sqlPublisherId = "SELECT @@Identity";
                    cmd.CommandText = sqlPublisherId;
                    int gameId = int.Parse(cmd.ExecuteScalar().ToString());

                    string sqlGenre = "INSERT INTO VideoGameGenre(GameId, GenreId) VALUES (@gameId, @genreId)";
                    cmd.CommandText = sqlGenre;

                    for (int i = 0; i < GenresSelected.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue("@gameId", gameId);
                        cmd.Parameters.AddWithValue("@genreId", GenresSelected[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    string sqlConsole = "INSERT INTO VideoGameConsole(GameId, ConsoleId) VALUES (@gameId, @consoleId)";
                    cmd.CommandText = sqlConsole;

                    for (int i = 0; i < ConsolesSelected.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue("@gameId", gameId);
                        cmd.Parameters.AddWithValue("@consoleId", ConsolesSelected[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

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

