
namespace MarkdownProccesor.Tokens.Abstract;

public interface IParent
{
    CompositeNode? Parent { get; }
}

