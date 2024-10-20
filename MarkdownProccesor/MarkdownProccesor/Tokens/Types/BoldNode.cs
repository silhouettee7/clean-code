

using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Tokens.Types;

public class BoldNode : CompositeNode
{
    public override NodeType TypeOfNode => NodeType.Bold;
    public override string Value => "strong";
    public BoldNode(CompositeNode parent) : base(parent) { }

}