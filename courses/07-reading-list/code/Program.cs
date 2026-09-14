// Course 7 - Reading List: serializing a List<Book> to a JSON file and
// reading it back, proving the data survives between runs.

using System.Text.Json;

const string FilePath = "books.json";

var books = new List<Book>
{
    new Book("Dune", "Frank Herbert"),
    new Book("Project Hail Mary", "Andy Weir"),
};

SaveBooks(books, FilePath);
Console.WriteLine($"Saved {books.Count} books to {FilePath}.");

var loadedBooks = LoadBooks(FilePath);
Console.WriteLine($"Loaded {loadedBooks.Count} books from {FilePath}:");
foreach (var book in loadedBooks)
{
    Console.WriteLine($"- {book.Title} by {book.Author} (finished: {book.IsFinished})");
}

// --- Challenge: read, modify, save, and reload to confirm it stuck ---
loadedBooks[0].IsFinished = true;
SaveBooks(loadedBooks, FilePath);

var reloadedBooks = LoadBooks(FilePath);
Console.WriteLine($"After marking \"{reloadedBooks[0].Title}\" as finished and reloading:");
foreach (var book in reloadedBooks)
{
    Console.WriteLine($"- {book.Title} by {book.Author} (finished: {book.IsFinished})");
}

void SaveBooks(List<Book> booksToSave, string path)
{
    string json = JsonSerializer.Serialize(booksToSave);
    File.WriteAllText(path, json);
}

List<Book> LoadBooks(string path)
{
    string json = File.ReadAllText(path);
    return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
}
