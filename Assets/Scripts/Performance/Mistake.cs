public class Mistake
{
    public readonly string Title;
    public readonly string Description;
    public readonly string Hint;
    public readonly int Sufix;

    public Mistake(string title, string description, string hint)
    {
        Title = title;
        Description = description;
        Hint = hint;
        Sufix = -1;
    }

    public Mistake(string title, string description, string hint, int sufix)
    {
        Title = title;
        Description = description;
        Hint = hint;
        Sufix = sufix;
    }
}

