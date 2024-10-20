
using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Tokens.Types;
public class DocumentNode : CompositeNode
{
    public override NodeType TypeOfNode => NodeType.Document;
    public override string? Value => "html";
}
