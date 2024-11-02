using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.Handlers.Tools;
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
        var text = new StringBuilder();
        while (word.Current != "_" && !word.IsProcessed)
        {
            if (word.Current == @"\") text.Append(EscapeSymbolHelper.HandleEscapeSymbols(word));
            else
            {
                text.Append(word.Current);
                word.AddCurrentIndexValue();
            }   
        }
        if (word.IsProcessed)
        {
            return HandleNodesHelper.HandleEndWordIfNoClosingTag(currentNode, text.ToString());
        }
        else
        {
            if (text.Length != 0) currentNode.Add(new TextNode(text.ToString()));
            return Successor.HandleWord(word, currentNode);
        }
    }
}
