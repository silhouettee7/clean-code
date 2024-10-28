
using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Handlers;
public class AllHandwritingHandler : INodeHandler
{
    public INodeHandler Successor { get ; set ; }

    public CompositeNode HandleWord(ProcessedWord word, CompositeNode currentNode)
    {
        if (word.Value.All((elem) => elem == '_'))
        {
            currentNode.Add(new TextNode(word.Value));
            return currentNode;
        }
        return Successor.HandleWord(word, currentNode);
    }
}