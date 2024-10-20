
using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;


namespace MarkdownProccesor.Handlers;

public class BoldNodeHandler : NodeWithPairTagInMarkdownHandler
{
    public BoldNodeHandler(Stack<string> tags) : base(tags) { }

    public override void HandleLine(ProcessedLineOfWords line, CompositeNode currentNode)
    {
        var currentWord = line.Current;
        HandleWord(line, currentWord, currentNode);
    }

    public override void HandleWord(ProcessedLineOfWords line, ProcessedWord word, CompositeNode currentNode)
    {
        if (word.IsBeginning && word.CurrentChar == '_' && word.NextChar == '_')
        {
            var boldNode = new BoldNode(currentNode);
            _tags.Push("__");
            word.AddCurrentIndexValue(2);
            if (word.IsProcessed)
            {
                var success = line.TryGoToNextWord();
                if (!success) return;
                Successor.HandleLine(line, currentNode);
            }
            else
            {
                Successor.HandleWord(line, word, boldNode);
            }
        }
        else if (word.IsEndForBold && word.CurrentChar == '_' && word.NextChar == '_')
        {
            if (_tags.Count != 0)
            {
                if (_tags.Peek() == "__")
                {
                    _tags.Pop();
                    var success = line.TryGoToNextWord();
                    if (!success) return;
                    Successor.HandleLine(line, currentNode.Parent);
                }
            }
        }
        else if (word.CurrentChar == '_' && word.NextChar == '_')
        {
            if (_tags.Count != 0)
            {
                if (_tags.Peek() == "__")
                {
                    _tags.Pop();
                    Successor.HandleWord(line, word, currentNode.Parent);
                    return;
                }
            }
            var boldNode = new BoldNode(currentNode);
            _tags.Push("__");
            Successor.HandleWord(line, word, boldNode);
        }
        else
        {
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
}
