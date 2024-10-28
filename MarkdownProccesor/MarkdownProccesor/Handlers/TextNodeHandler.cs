using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;
using System.Text;

namespace MarkdownProccesor.Handlers;
internal class TextNodeHandler : INodeHandler
{
    public INodeHandler Successor { get ; set ; }

    public CompositeNode HandleWord(ProcessedWord word, CompositeNode currentNode)
    {
        word.ContextNode = NodeType.Text;
        var currentChar = word.Current;
        var text = new StringBuilder();
        while (currentChar != "_" && !word.IsProcessed)
        {
            text.Append(currentChar);
            word.AddCurrentIndexValue();
            currentChar = word.Current;
        }
        if (word.IsProcessed)
        {
            currentNode.Add(new TextNode(text.ToString() + ' '));
            if (currentNode.TypeOfNode == NodeType.Italic && currentNode.IsBeginInWord)
            {
                var textNode = new TextNode("_" + currentNode.RepresentWithoutTags());
                currentNode.Parent.Remove(currentNode);
                currentNode = currentNode.Parent;
                currentNode.Add(textNode);
            }
            if (currentNode.TypeOfNode == NodeType.Bold && currentNode.IsBeginInWord)
            {
                var textNode = new TextNode("__" + currentNode.RepresentWithoutTags());
                currentNode.Parent.Remove(currentNode);
                currentNode = currentNode.Parent;
                currentNode.Add(textNode);
            }
            return currentNode;
        }
        else
        {
            if (text.Length != 0) currentNode.Add(new TextNode(text.ToString()));
            return Successor.HandleWord(word, currentNode);
        }
    }
}
