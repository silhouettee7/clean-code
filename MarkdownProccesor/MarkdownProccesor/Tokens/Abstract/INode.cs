
namespace MarkdownProccesor.Tokens.Abstract;
public interface INode
{
    string? Value { get; }
    string? Represent();
}

