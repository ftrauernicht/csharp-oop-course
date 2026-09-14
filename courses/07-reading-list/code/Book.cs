public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsFinished { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        IsFinished = false;
    }
}
