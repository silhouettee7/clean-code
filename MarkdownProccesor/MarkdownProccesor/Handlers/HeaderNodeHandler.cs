
using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Handlers;

internal class HeaderNodeHandler : INodeHandler
{
    public INodeHandler? Successor { get; set; }

    public void HandleLine(ProcessedLineOfWords line, CompositeNode currentNode)
    {
        var firstWord = line.FirstWord;
        if (firstWord == null) throw new NullReferenceException();
        var success = line.TryGoToNextWord();
        if (!success) return;
        HandleWord(line, firstWord, currentNode);

    }

    public void HandleWord(ProcessedLineOfWords line, ProcessedWord word, CompositeNode currentNode)
    {
        if (word.Value.All((element) => element == '#'))
        {
            var headerNode = new HeaderNode(currentNode, (uint)word.Value.Length);
            Successor.HandleLine(line, headerNode);
        }
        else
        {
            currentNode.Add(new TextNode(word.Value));
            Successor.HandleLine(line, currentNode);
        }

    }
}
