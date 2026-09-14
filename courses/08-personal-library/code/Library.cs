public class Library
{
    private readonly List<Book> _books = new List<Book>();

    public IReadOnlyList<Book> Books
    {
        get { return _books.AsReadOnly(); }
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
        Console.WriteLine($"Added \"{book.Title}\" to the library.");
    }

    public bool RemoveBook(string title)
    {
        for (int i = 0; i < _books.Count; i++)
        {
            if (_books[i].Title == title)
            {
                _books.RemoveAt(i);
                Console.WriteLine($"Removed \"{title}\" from the library.");
                return true;
            }
        }

        Console.WriteLine($"No book titled \"{title}\" found.");
        return false;
    }
}
