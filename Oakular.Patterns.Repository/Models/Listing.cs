namespace Oakular.Patterns.Repository.Models;

public readonly struct Listing : INameable
{
    public Listing(string name)
    {
        Name = name.Replace(" ", "-");
    }

    public string Name { get; }
}
