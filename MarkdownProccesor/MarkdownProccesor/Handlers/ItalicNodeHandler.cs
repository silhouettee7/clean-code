using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Factories;

namespace MarkdownProccesor.Handlers;

public class ItalicNodeHandler : ItalicWithBoldTagsHandler
{
    public override NodeType TagType => NodeType.Italic;
    public override CompositeNodeFactory Factory => new ItalicNodeFactory();
}
public class ItalicNodeHandlerr : INodeHandler
{
    public INodeHandler Successor { get ; set; }

    public CompositeNode HandleWord(ProcessedWord word, CompositeNode currentNode)
    {
        word.ContextNode = NodeType.Italic;
        if (word.IsBeginning && word.Current == "_")
        {
            var italicNode = new ItalicNode(currentNode);
            word.AddCurrentIndexValue();
            return Successor.HandleWord(word, italicNode);
        }
        else if (word.IsEnd && word.Current == "_")
        {
            if (currentNode.TypeOfNode == NodeType.Italic)
            {
                var tempNode = currentNode.Parent;
                while (tempNode != null)
                {
                    if (tempNode.TypeOfNode == NodeType.Italic) break;
                    tempNode = tempNode.Parent;
                }
                if (tempNode != null)
                {
                    tempNode.Parent.Remove(tempNode);
                    tempNode.Parent.Add(new TextNode("_" + tempNode.RepresentWithoutTags()));
                    return tempNode.Parent;
                }
                return currentNode.Parent;
            }
            else if (currentNode.TypeOfNode == NodeType.Bold)
            {
                var textNode = new TextNode("__" + currentNode.RepresentWithoutTags() + '_');
                currentNode.Parent.Remove(currentNode);
                currentNode.Parent.Add(textNode);              
                return currentNode.Parent;
            }
            else
            {
                currentNode.Add(new TextNode("_"));
                
                return currentNode.Parent;
            }
           
        }
        else if (word.Current == "_")
        {
            word.AddCurrentIndexValue();
            if (currentNode.TypeOfNode == NodeType.Italic)
            { 
                return Successor.HandleWord(word, currentNode.Parent);
            }
            else if (currentNode.TypeOfNode == NodeType.Bold && currentNode.IsBeginInWord)
            {
                var textNode = new TextNode("__" + currentNode.RepresentWithoutTags() + '_');
                currentNode.Parent.Remove(currentNode);
                currentNode.Parent.Add(textNode);
                return Successor.HandleWord(word, currentNode.Parent);
            }
            else
            {
                var italicNode = new ItalicNode(currentNode);
                italicNode.IsBeginInWord = true;
                return Successor.HandleWord(word, italicNode);
            }
            
        }
        else
        {
            if (word.IsProcessed)
            {
                return currentNode;
            }
            return Successor.HandleWord(word, currentNode);
        }
    }
    
}
