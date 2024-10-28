using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Tokens.Types;

public sealed class TextNode : INode
{
    public string? Value { get; }
    public string? Represent() => Value;
    public TextNode(string text)
    {
        Value = text;
    }
    public static TextNode DecorateCurrentNodeToTextNode(CompositeNode node, NodeType leftTag, NodeType rightTag)
    {
        return new TextNode(TagMatching.NodeTypeMatching[leftTag].mdTag
            + node.RepresentWithoutTags()
            + TagMatching.NodeTypeMatching[rightTag].mdTag);
    }
}
