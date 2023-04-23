namespace Oakular.Patterns.Repository.Models;

public readonly struct Composite : INameable
{
    public Composite(string name)
    {
        Name = name;
    }

    public string Name { get; }
}