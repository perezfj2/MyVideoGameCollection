using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyVideoGameUI.Models;

namespace MyVideoGameUI.Pages.VideoGames
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<VideoGame> GameList { get; set; } = new List<VideoGame>();
        public List<VideoGameDetail> GameDetailList{ get; set; } = new List<VideoGameDetail>();
        public void OnGet()
        {
            
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT GameId, GameImageURL, GameTitle, PublisherName,LanguageName, PlayerModeName, ContentRatingName, GameSummary, GameRating " +
                "FROM VideoGame INNER JOIN Publisher ON VideoGame.PublisherId = Publisher.PublisherId " +
                "INNER JOIN Language ON VideoGame.LanguageId = Language.LanguageId " +
                "INNER JOIN PLayerMode ON VideoGame.PlayerModeId = PlayerMode.PlayerModeId " +
                "INNER JOIN ContentRating ON VideoGame.ContentRatingId = ContentRating.ContentRatingId Order by GameTitle";

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
                        videoGameDetail.PublisherName = reader["PublisherName"].ToString();
                        videoGameDetail.GameSummary = reader["GameSummary"].ToString();
                        videoGameDetail.GameRating = reader["GameRating"].ToString();
                        videoGameDetail.LanguageName = reader["LanguageName"].ToString();
                        videoGameDetail.PlayerModeName = reader["PlayerModeName"].ToString();
                        videoGameDetail.ContentRatingName = reader["ContentRatingName"].ToString();
                        GameDetailList.Add(videoGameDetail);

                    }
                }
                reader.Close();

                string sql2 = "SELECT * FROM VideoGame INNER JOIN Publisher ON VideoGame.PublisherId = Publisher.PublisherId Order by GameTitle";

                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        VideoGame videoGame = new VideoGame();
                        videoGame.GameId = int.Parse(reader2["GameId"].ToString());
                        videoGame.GameImageURL = reader2["GameImageURL"].ToString();
                        videoGame.GameTitle = reader2["GameTitle"].ToString();
                        videoGame.GameSummary = reader2["GameSummary"].ToString();
                        videoGame.GameRating = decimal.Parse(reader2["GameRating"].ToString());
                        videoGame.PublisherId = int.Parse(reader2["PublisherId"].ToString());
                        videoGame.LanguageId = int.Parse(reader2["LanguageId"].ToString());
                        videoGame.ReleaseDate = (DateTime)reader2["ReleaseDate"];
                        videoGame.PlayerModeId = int.Parse(reader2["PlayerModeId"].ToString());
                        videoGame.ContentRatingId = int.Parse(reader2["ContentRatingId"].ToString());

                        GameList.Add(videoGame);

                    }
                }
            }
        }

        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM VideoGame WHERE GameId = @GameId"; //delete sql statement
                SqlCommand cmd = new SqlCommand(sql, conn); //make command
                cmd.Parameters.AddWithValue("@GameId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");

            }
        }

    }
}
