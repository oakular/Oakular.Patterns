namespace Oakular.Patterns.Web.Options;

public sealed record Authentication
{
    public string ClientId { get; init; } = default!;
}