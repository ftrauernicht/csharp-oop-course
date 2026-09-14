public class Song
{
    public string Title { get; }
    public string Artist { get; }
    public int DurationSeconds { get; }
    public string Genre { get; }

    public Song(string title, string artist, int durationSeconds, string genre)
    {
        Title = title;
        Artist = artist;
        DurationSeconds = durationSeconds;
        Genre = genre;
    }
}
