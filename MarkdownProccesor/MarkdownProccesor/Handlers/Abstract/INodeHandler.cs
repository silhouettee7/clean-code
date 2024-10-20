
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Handlers.Abstract;
public interface INodeHandler
{
    INodeHandler? Successor { get; set; }
    void HandleLine(ProcessedLineOfWords line, CompositeNode currentNode);
    void HandleWord(ProcessedLineOfWords line, ProcessedWord word, CompositeNode currentNode);
}