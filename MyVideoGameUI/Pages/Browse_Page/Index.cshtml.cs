using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyVideoGameUI.Models;
using Microsoft.Data.SqlClient;

namespace MyVideoGameUI.Pages.Browse_Page
{
    public class IndexModel : PageModel
    {
        public List<VideoGameDetail> GameDetailList { get; set; } = new List<VideoGameDetail>();
        public List<VideoGameDetail> GameActionList { get; set; } = new List<VideoGameDetail>();
        public List<VideoGameDetail> GameAdventureList { get; set; } = new List<VideoGameDetail>();
        public List<VideoGameDetail> GameSinglePlayerList { get; set; } = new List<VideoGameDetail>();
        public List<VideoGameDetail> GameOnlineList { get; set; } = new List<VideoGameDetail>();

        public void OnGet()
        {
            //adds list for finding alphabetical
            PopulateAlphaList();
            PopulateActionList();
            PopulateAdventureList();
            PopulateOnlineList();
            PopulateSinglePlayerList();

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

        public void PopulateActionList()
        {
            //adds list for finding alphabetical
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT vg.GameId, vg.GameImageURL, vg.GameTitle " +
                "FROM VideoGame vg INNER JOIN VideoGameGenre vgg ON vg.GameId = vgg.GameId " +
                "INNER JOIN Genre g ON vgg.GenreId = g.GenreId " +
                "WHERE g.GenreName = 'Action' " +
                "ORDER BY vg.GameTitle";

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

                        GameActionList.Add(videoGameDetail);

                    }
                }
                reader.Close();

            } //end using
        }

        public void PopulateAdventureList()
        {
            //adds list for finding alphabetical
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT vg.GameId, vg.GameImageURL, vg.GameTitle " +
                "FROM VideoGame vg INNER JOIN VideoGameGenre vgg ON vg.GameId = vgg.GameId " +
                "INNER JOIN Genre g ON vgg.GenreId = g.GenreId " +
                "WHERE g.GenreName = 'Adventure' " +
                "ORDER BY vg.GameTitle";

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

                        GameAdventureList.Add(videoGameDetail);

                    }
                }
                reader.Close();

            } //end using
        }

        public void PopulateSinglePlayerList()
        {
            //adds list for finding alphabetical
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT vg.GameId, vg.GameImageURL, vg.GameTitle " +
                "FROM VideoGame vg INNER JOIN PlayerMode pm ON vg.PlayerModeId = pm.PlayerModeId " +
                "WHERE pm.PlayerModeName = 'Single Player' " +
                "ORDER BY vg.GameTitle";

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

                        GameSinglePlayerList.Add(videoGameDetail);

                    }
                }
                reader.Close();

            } //end using
        }

        public void PopulateOnlineList()
        {
            //adds list for finding alphabetical
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT vg.GameId, vg.GameImageURL, vg.GameTitle " +
                "FROM VideoGame vg INNER JOIN PlayerMode pm ON vg.PlayerModeId = pm.PlayerModeId " +
                "WHERE pm.PlayerModeName = 'Online Player' " +
                "ORDER BY vg.GameTitle";

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

                        GameOnlineList.Add(videoGameDetail);

                    }
                }
                reader.Close();

            } //end using
        }
    }
}
