using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Tokens.Types;

public sealed class TextNode : INode, IParent
{
    public string? Value { get; }
    public CompositeNode? Parent { get; }
    public string? Represent() => Value;
    public TextNode(string text)
    {
        Value = text;
    }
}
