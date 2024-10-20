using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;
using System.Text;

namespace MarkdownProccesor.Handlers;
internal class TextNodeHandler : INodeHandler
{
    public INodeHandler? Successor { get ; set ; }

    public void HandleLine(ProcessedLineOfWords line, CompositeNode currentNode)
    {
        var currentWord = line.Current;
        HandleWord(line, currentWord, currentNode);
    }

    public void HandleWord(ProcessedLineOfWords line, ProcessedWord word, CompositeNode currentNode)
    {
        var currentChar = word.CurrentChar;
        var text = new StringBuilder();
        while (currentChar != '_' && !word.IsProcessed)
        {
            text.Append(currentChar);
            word.AddCurrentIndexValue(1);
            currentChar = word.CurrentChar;
        }
        currentNode.Add(new TextNode(text.ToString() + ' '));
        if (word.IsProcessed)
        {
            var success = line.TryGoToNextWord();
            if (!success) return;
            Successor.HandleLine(line, currentNode);
        }
        else
        {
            Successor.HandleWord(line, word, currentNode);
        }
    }
}
