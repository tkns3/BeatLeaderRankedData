﻿using get_ranked_maps.APIs;
using Newtonsoft.Json;

if (args.Length != 1)
{
    throw new ArgumentException("args length should be one.");
}
var outputJsonPath = args[0];

HttpUtility.HttpClient.DefaultRequestHeaders.Add("User-Agent", "tkns3.get-ranked-maps");

List<MapInfo> mapInfos = new List<MapInfo>();

for (int page = 1; ; page++)
{
    var res = await BeatLeader.GetLeaderboards(page: page, count: 4000, sortBy: "stars", order: "desc", type: "ranked", stars_from: 0, stars_to: 18);
    if (res.data.Length == 0)
    {
        break;
    }
    foreach (var l in res.data)
    {
        if (
            l.id == null
            || l.song.hash == null
            || l.difficulty.difficultyName == null
            || l.difficulty.modeName == null
            || l.difficulty.stars == null
            )
        {
            continue;
        }
        var beatSaverId = l.id ?? "91";

        mapInfos.Add(new MapInfo()
        {
            BeatLeaderId = l.id ?? "",
            BeatSaverId = beatSaverId.Replace("x","")[0..^2],
            Hash = l.song.hash ?? "",
            SongName = l.song.name ?? "",
            SongSubName = l.song.subName ?? "",
            SongAuthorName = l.song.author ?? "",
            MapperName = l.song.mapper ?? "",
            UploadedTime = new DateTime(1970, 1, 1).AddSeconds((double)l.song.uploadTime).ToString("yyyy-MM-ddTHH:mm:ssZ"),
            Bpm = l.song.bpm,
            Duration = l.song.duration,
            DifficultyName = l.difficulty.difficultyName ?? "",
            ModeName = l.difficulty.modeName ?? "",
            Stars = l.difficulty.stars ?? 0,
            Ranked = true,
            RankedTime = new DateTime(1970, 1, 1).AddSeconds((double)l.difficulty.rankedTime).ToString("yyyy-MM-ddTHH:mm:ssZ"),
            Bombs = l.difficulty.bombs,
            Notes = l.difficulty.notes,
            Walls = l.difficulty.walls,
            Njs = l.difficulty.njs,
            Nps = l.difficulty.nps,
            MaxScore = l.difficulty.maxScore,
        });
    }
}

var s = JsonConvert.SerializeObject(mapInfos, Formatting.Indented);
File.WriteAllText(outputJsonPath, s);

class MapInfo
{
    public string BeatLeaderId { get; set; } = "";
    public string BeatSaverId { get; set; } = "";
    public string Hash { get; set; } = "";
    public string SongName { get; set; } = "";
    public string SongSubName { get; set; } = "";
    public string SongAuthorName { get; set; } = "";
    public string MapperName { get; set; } = "";
    public string UploadedTime { get; set; } = "";
    public double Bpm { get; set; }
    public double Duration { get; set; }
    public string DifficultyName { get; set; } = "";
    public string ModeName { get; set; } = "";
    public double Stars { get; set; }
    public bool Ranked { get; set; }
    public string RankedTime { get; set; } = "";
    public int Bombs { get; set; }
    public int Notes { get; set; }
    public int Walls { get; set; }
    public double Njs { get; set; }
    public double Nps { get; set; }
    public int MaxScore { get; set; }
}
