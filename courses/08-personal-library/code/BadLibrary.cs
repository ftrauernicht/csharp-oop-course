// The problem this chapter fixes: exposing the list itself means outside
// code can bypass any bookkeeping entirely -- or wipe it out completely.
public class BadLibrary
{
    public List<Book> Books { get; } = new List<Book>();
}
