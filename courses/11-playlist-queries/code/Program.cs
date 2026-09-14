// Course 11 - Playlist Queries: filtering, sorting, and summarizing a
// list declaratively with LINQ, instead of a manual loop for each one.

var songs = new List<Song>
{
    new Song("Song A", "Artist 1", 210, "Rock"),
    new Song("Song B", "Artist 2", 340, "Jazz"),
    new Song("Song C", "Artist 1", 180, "Rock"),
    new Song("Song D", "Artist 3", 275, "Pop"),
};

// --- The old way: foreach + if ---
var rockSongsOld = new List<Song>();
foreach (var song in songs)
{
    if (song.Genre == "Rock")
    {
        rockSongsOld.Add(song);
    }
}
Console.WriteLine($"Rock songs (manual): {rockSongsOld.Count}");

// --- The LINQ way: Where ---
var rockSongs = songs.Where(s => s.Genre == "Rock").ToList();
Console.WriteLine($"Rock songs (LINQ): {rockSongs.Count}");

// --- Select: projecting to titles only ---
var titles = songs.Select(s => s.Title).ToList();
Console.WriteLine("Titles: " + string.Join(", ", titles));

// --- OrderBy ---
var byDuration = songs.OrderBy(s => s.DurationSeconds).ToList();
foreach (var song in byDuration)
{
    Console.WriteLine($"{song.Title}: {song.DurationSeconds}s");
}

// --- Aggregations ---
int totalDuration = songs.Sum(s => s.DurationSeconds);
Console.WriteLine($"Total playlist duration: {totalDuration}s");

// --- Core exercise: Where + OrderByDescending combined ---
var longSongsDescending = songs
    .Where(s => s.DurationSeconds > 200)
    .OrderByDescending(s => s.DurationSeconds)
    .ToList();
foreach (var song in longSongsDescending)
{
    Console.WriteLine($"Long: {song.Title} ({song.DurationSeconds}s)");
}

// --- Challenge: GroupBy ---
var byGenre = songs.GroupBy(s => s.Genre);
foreach (var group in byGenre)
{
    Console.WriteLine($"{group.Key}: {group.Count()} song(s)");
}
