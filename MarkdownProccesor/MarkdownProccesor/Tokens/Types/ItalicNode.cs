

using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Tokens.Types;
internal class ItalicNode : CompositeNode
{
    public override NodeType TypeOfNode => NodeType.Italic;
    public override string Value => "em";
    public ItalicNode(CompositeNode parent) : base(parent) { }
    
}
