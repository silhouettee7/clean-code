
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Handlers.Abstract;
public interface INodeHandler
{
    INodeHandler Successor { get; set; }
    CompositeNode HandleWord(ProcessedWord word, CompositeNode currentNode);
}