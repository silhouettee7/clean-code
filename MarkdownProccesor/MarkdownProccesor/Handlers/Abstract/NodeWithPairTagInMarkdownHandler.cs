
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Handlers.Abstract;

public abstract class NodeWithPairTagInMarkdownHandler : INodeHandler
{
    protected Stack<string> _tags;
    public INodeHandler? Successor { get; set; }
    public abstract void HandleLine(ProcessedLineOfWords line, CompositeNode currentNode);
    public abstract void HandleWord(ProcessedLineOfWords line, ProcessedWord word, CompositeNode currentNode);

    public NodeWithPairTagInMarkdownHandler(Stack<string> tags)
    {
        _tags = tags;
    }
}