using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.VideoGames
{
    public class DetailPageModel : PageModel
    {
        public List<VideoGameDetail> GameDisplayList { get; set; } = new List<VideoGameDetail>();
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
            //PopulateVideoGameInfo();
            PopulatePublisherList();
            PopulateLanguageList();
            PopulatePlayerModeList();
            PopulateContentRatingList();
            PopulateGenreList();
            PopulateConsoleList();

            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString())) //set up connection
            {

                string sql = "SELECT GameId, GameImageURL, GameTitle, PublisherName,LanguageName, PlayerModeName, ContentRatingName, GameSummary, ReleaseDate, GameRating " +
                "FROM VideoGame INNER JOIN Publisher ON VideoGame.PublisherId = Publisher.PublisherId " +
                "INNER JOIN Language ON VideoGame.LanguageId = Language.LanguageId " +
                "INNER JOIN PLayerMode ON VideoGame.PlayerModeId = PlayerMode.PlayerModeId " +
                "INNER JOIN ContentRating ON VideoGame.ContentRatingId = ContentRating.ContentRatingId " +
                "WHERE GameId = @gameId " +
                "Order by GameTitle";


                //step 3 - SQL command object
                SqlCommand cmd = new SqlCommand(sql, conn); //set up a command
                cmd.Parameters.AddWithValue("@gameId", id); //pass in parameter value

                //step 4 - open connection
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(); //put it in the reader
                if (reader.HasRows) //check if there is anything in the reader
                {
                    reader.Read(); //read the first record
                    ExistingVideoGameDetail.GameId = int.Parse(reader["GameId"].ToString());
                    ExistingVideoGameDetail.GameTitle = reader["GameTitle"].ToString();
                    ExistingVideoGameDetail.GameImageURL = reader["GameImageURL"].ToString();
                    ExistingVideoGameDetail.GameSummary = reader["GameSummary"].ToString();
                    ExistingVideoGameDetail.PublisherName = reader["PublisherName"].ToString();
                    ExistingVideoGameDetail.GameRating = reader["GameRating"].ToString();
                    ExistingVideoGameDetail.LanguageName = reader["LanguageName"].ToString();
                    ExistingVideoGameDetail.ReleaseDate = reader["ReleaseDate"].ToString();
                    ExistingVideoGameDetail.PlayerModeName = reader["PlayerModeName"].ToString();
                    ExistingVideoGameDetail.ContentRatingName = reader["ContentRatingName"].ToString();
                    GameDisplayList.Add(ExistingVideoGameDetail);
                }

            }

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

            //retrieve a list of VideoGameGenre records {gameId, genreId} from database.
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlGenres = "SELECT GenreId FROM VideoGameGenre WHERE GameId = @gameId";
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

    

