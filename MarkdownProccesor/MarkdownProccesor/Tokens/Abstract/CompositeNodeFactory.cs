

namespace MarkdownProccesor.Tokens.Abstract;
public abstract class CompositeNodeFactory
{
    public abstract CompositeNode CreateCompositeNode(CompositeNode parent);
}
