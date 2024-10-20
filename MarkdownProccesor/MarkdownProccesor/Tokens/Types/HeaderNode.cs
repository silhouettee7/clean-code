using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Tokens.Types;

public sealed class HeaderNode : CompositeNode
{
    private uint _levelOfHeader;
    public override NodeType TypeOfNode => NodeType.Header;
    public override string? Value => "h";
    public override string Represent() => $"<{Value}{_levelOfHeader}>{RepresentAllChildrenNodes()}</{Value}{_levelOfHeader}>";
    public HeaderNode(CompositeNode parent, uint level): base(parent)
    {
        _levelOfHeader = level;
    }
}
