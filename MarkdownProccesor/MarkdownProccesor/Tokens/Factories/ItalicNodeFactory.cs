

using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Tokens.Factories;
public class ItalicNodeFactory : CompositeNodeFactory
{
    public override CompositeNode CreateCompositeNode(CompositeNode parent)
    {
        return new ItalicNode(parent);
    }
}
