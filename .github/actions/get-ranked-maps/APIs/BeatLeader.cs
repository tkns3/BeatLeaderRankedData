using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace get_ranked_maps.APIs
{
    internal static class BeatLeader
    {
        private static string _urlbase = "https://api.beatleader.xyz";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="sortBy">null (=??) / "stars" / "name" / "timestamp" / "voting" / "voteratio" / "votecount" / "playcount" / "scoreTime"</param>
        /// <param name="order">null (=??) / "desc" / "asc"</param>
        /// <param name="search"></param>
        /// <param name="type">null (=??) / "all" / "nominated" / "qualified" / "ranked" / "reweighting" / "reweighted" / "unranked"</param>
        /// <param name="mapType">null (=??) / 1: Acc, 2: Tech, 4: Midspeed, 8: Speed</param>
        /// <param name="allTypes">0: ANY category / 1: ALL categories / 2: NO categories</param>
        /// <param name="mytype">null (=All maps) / "played" / "unplayed"</param>
        /// <param name="stars_from"></param>
        /// <param name="stars_to"></param>
        /// <param name="date_from"></param>
        /// <param name="date_to"></param>
        /// <returns></returns>
        public static async Task<LeaderboardInfoResponseWithMetadata> GetLeaderboards(int page = 1, int count = 10, string? sortBy = null, string? order = null, string? search = null, string? type = null, int? mapType = null, int? allTypes = null, string? mytype = null, double? stars_from = null, double? stars_to = null, int? date_from = null, int? date_to = null)
        {
            var opt_page       = $"page={page}";
            var opt_count      = $"&count={count}";
            var opt_sortBy     = (sortBy == null)     ? "" : $"&sortBy={sortBy}";
            var opt_order      = (order == null)      ? "" : $"&order={order}";
            var opt_search     = (search == null)     ? "" : $"&search={search}";
            var opt_type       = (type == null)       ? "" : $"&type={type}";
            var opt_mapType    = (mapType == null)    ? "" : $"&mapType={mapType}";
            var opt_allTypes   = (allTypes == null)   ? "" : $"&allTypes={allTypes}";
            var opt_mytype     = (mytype == null)     ? "" : $"&mytype={mytype}";
            var opt_stars_from = (stars_from == null) ? "" : $"&stars_from={stars_from}";
            var opt_stars_to   = (stars_to == null)   ? "" : $"&stars_to={stars_to}";
            var opt_date_from  = (date_from == null)  ? "" : $"&date_from={date_from}";
            var opt_date_to    = (date_to == null)    ? "" : $"&date_to={date_to}";
            var url = $"{_urlbase}/leaderboards?{opt_page}{opt_count}{opt_sortBy}{opt_order}{opt_search}{opt_type}{opt_mapType}{opt_allTypes}{opt_mytype}{opt_stars_from}{opt_stars_to}{opt_date_from}{opt_date_to}";

            var res = await HttpUtility.GetAndDeserialize<LeaderboardInfoResponseWithMetadata>(url);
            return res ?? new LeaderboardInfoResponseWithMetadata();
        }

        public class LeaderboardInfoResponseWithMetadata
        {
            public Metadata metadata { get; set; } = new();
            public LeaderboardInfoResponse[] data { get; set; } = Array.Empty<LeaderboardInfoResponse>();
        }

        public class DifficultyDescription
        {
            public int id { get; set; }
            public int value { get; set; }
            public int mode { get; set; }
            public string? difficultyName { get; set; }
            public string? modeName { get; set; }
            public int status { get; set; }
            public ModifiersMap modifierValues { get; set; } = new();
            public int nominatedTime { get; set; }
            public int qualifiedTime { get; set; }
            public int rankedTime { get; set; }
            public double? stars { get; set; }
            public int type { get; set; }
            public double njs { get; set; }
            public double nps { get; set; }
            public int notes { get; set; }
            public int bombs { get; set; }
            public int walls { get; set; }
            public int maxScore { get; set; }
            public double duration { get; set; }
        }

        public class ModifiersMap
        {
            public int modifierId { get; set; }
            public double da { get; set; }
            public double fs { get; set; }
            public double ss { get; set; }
            public double sf { get; set; }
            public double gn { get; set; }
            public double na { get; set; }
            public double nb { get; set; }
            public double nf { get; set; }
            public double no { get; set; }
            public double pm { get; set; }
            public double sc { get; set; }
            public double sa { get; set; }
        }

        public class Metadata
        {
            public int itemsPerPage { get; set; }
            public int page { get; set; }
            public int total { get; set; }
        }

        public class LeaderboardInfoResponse
        {
            public string? id { get; set; }
            public Song song { get; set; } = new();
            public DifficultyDescription difficulty { get; set; } = new();
            public int plays { get; set; }
            public int positiveVotes { get; set; }
            public int starVotes { get; set; }
            public int negativeVotes { get; set; }
            public double voteStars { get; set; }
            public ScoreResponseWithAcc? myScore { get; set; }
            public RankQualification? qualification { get; set; }
            public RankUpdate? reweight { get; set; }
        }

        public class RankQualification
        {
            public int id { get; set; }
            // 他プロパティは省略

        }

        public class RankUpdate
        {
            public int id { get; set; }
            // 他プロパティは省略

        }

        public class ScoreResponseWithAcc
        {
            public int id { get; set; }
            // 他プロパティは省略
        }

        public class Song
        {
            public string? id { get; set; }
            public string? hash { get; set; }
            public string? name { get; set; }
            public string? description { get; set; }
            public string? subName { get; set; }
            public string? author { get; set; }
            public string? mapper { get; set; }
            public int mapperId { get; set; }
            public string? coverImage { get; set; }
            public string? downloadUrl { get; set; }
            public double bpm { get; set; }
            public double duration { get; set; }
            public string? tags { get; set; }
            public string? createdTime { get; set; }
            public int uploadTime { get; set; }
            public DifficultyDescription[] difficulties { get; set; } = Array.Empty<DifficultyDescription>();
        }
    }
}
