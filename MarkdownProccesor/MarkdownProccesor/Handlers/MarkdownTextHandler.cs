

using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.Tokens.Types;
using MarkdownProccesor.ProcessedObjects;
namespace MarkdownProccesor.Handlers;

public class MarkdownTextHandler
{
    private readonly INodeHandler _wordHandler;
    private readonly CompositeNode _root;
    private CompositeNode _currentNode;

    public MarkdownTextHandler(INodeHandler handler)
    {
        _root = new DocumentNode();
        _currentNode = _root;
        _wordHandler = handler;
    }
    public string HandleMdTextLines(string[] mdText)
    {
        foreach(var line in mdText)
        {
            var textSplitByWords = line.Split(new char[] { '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            HandleLine(textSplitByWords);
        }
        return _root.Represent();
    }
    private void HandleLine(string[] line)
    {
        //if (line.Length == 1 && line[0] == string.Empty)
        //{
        //    while (_currentNode.TypeOfNode == NodeType.Italic || 
        //        _currentNode.TypeOfNode == NodeType.Bold)
        //    {

        //    }
        //}
        for (int i = 0; i < line.Length; i++)
        {
            var word = new ProcessedWord(line[i]);
            if (i == 0) word.IsFirst = true;
            _currentNode = _wordHandler.HandleWord(word, _currentNode);
        }
    }
    
}
