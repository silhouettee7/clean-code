

using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Tokens.Factories;

public class BoldNodeFactory : CompositeNodeFactory
{
    public override CompositeNode CreateCompositeNode(CompositeNode parent)
    {
        return new BoldNode(parent);
    }
}
