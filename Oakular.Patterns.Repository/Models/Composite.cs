namespace Oakular.Patterns.Repository.Models;

public readonly struct Composite : INameable
{
    public Composite(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

public sealed class CompositeContent
{
    public CompositeContent(Stream stream, string contentType)
    {
        stream.Position = 0;
        
        Stream = stream;
        ContentType = contentType;
    }
    
    public Stream Stream { get; }
    public string ContentType { get; }
}