// A generic class: <T> is a placeholder for whatever type you use this
// with (Deck<Card>, Deck<int>, Deck<string>, ...) -- Deck<T> itself never
// needs to know which one.
public class Deck<T>
{
    private readonly List<T> _cards = new List<T>();

    public int Count
    {
        get { return _cards.Count; }
    }

    public void Add(T card)
    {
        _cards.Add(card);
    }

    public T Draw()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("The deck is empty.");
        }

        T card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public void Shuffle()
    {
        var random = new Random();
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            T temp = _cards[i];
            _cards[i] = _cards[j];
            _cards[j] = temp;
        }
    }
}
