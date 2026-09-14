// Course 8 - Personal Library: encapsulating a collection, not just a
// single value -- the collection-shaped version of Course 3's lesson.

var badLibrary = new BadLibrary();
badLibrary.Books.Add(new Book("Dune", "Frank Herbert"));
badLibrary.Books.Clear(); // nothing stops this -- the whole library, gone
Console.WriteLine($"BadLibrary book count after an outside Clear(): {badLibrary.Books.Count}");

var library = new Library();
library.AddBook(new Book("Dune", "Frank Herbert"));
library.AddBook(new Book("Project Hail Mary", "Andy Weir"));

foreach (var book in library.Books)
{
    Console.WriteLine($"- {book.Title} by {book.Author}");
}

library.RemoveBook("Dune");
Console.WriteLine($"Books remaining: {library.Books.Count}");
