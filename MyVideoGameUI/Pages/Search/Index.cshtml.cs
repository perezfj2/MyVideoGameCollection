using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyVideoGameUI.Pages.Search
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<VideoGameDetail> GameSearchList { get; set; } = new List<VideoGameDetail>();
        public List<VideoGameDetail> GameDetailList { get; set; } = new List<VideoGameDetail>();
        [BindProperty]
        public List<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> Languages { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> PlayerModes { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> ContentRatings { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ConsoleList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<int> ConsolesSelected { get; set; }

        [BindProperty]
        public List<SelectListItem> GenreList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<int> GenresSelected { get; set; }
        public void OnGet(int languageId)
        {
            PopulateAlphaList();
            PopulatePublisherList();
            PopulateLanguageList();
            PopulatePlayerModeList();
            PopulateContentRatingList();
            PopulateGenreList();
            PopulateConsoleList();

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
        }

        public void PopulateAlphaList()
        {
            //adds list for finding alphabetical
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT GameId, GameImageURL, GameTitle FROM VideoGame Order by GameTitle";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VideoGameDetail videoGameDetail = new VideoGameDetail();
                        videoGameDetail.GameId = int.Parse(reader["GameId"].ToString());
                        videoGameDetail.GameImageURL = reader["GameImageURL"].ToString();
                        videoGameDetail.GameTitle = reader["GameTitle"].ToString();

                        GameDetailList.Add(videoGameDetail);

                    }
                }
                reader.Close();

            } //end using
        }

        public void PopulateSearchList(int languageId)
        {
            //adds list for finding alphabetical
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT GameId, GameImageURL, GameTitle FROM VideoGame " +
                    "WHERE LanguageId = @languageId Order by GameTitle";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@languageId", SqlDbType.Int).Value = languageId;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VideoGameDetail videoGameDetail = new VideoGameDetail();
                        videoGameDetail.GameId = int.Parse(reader["GameId"].ToString());
                        videoGameDetail.GameImageURL = reader["GameImageURL"].ToString();
                        videoGameDetail.GameTitle = reader["GameTitle"].ToString();

                        GameSearchList.Add(videoGameDetail);

                    }
                }
                reader.Close();

            } //end using
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
    }
}
