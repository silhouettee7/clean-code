using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Types;
using static System.Net.Mime.MediaTypeNames;

namespace MarkdownProccesor.Handlers.Tools;
public static class HandleNodesHelper
{
    public static CompositeNode HandleEndWordIfNoClosingTag(CompositeNode node, string text)
    {
        node = HandleNoneClosingTag(node);
        node.Add(new TextNode(text+' '));
        return node;
        
    }
    public static CompositeNode HandleEndWordIfBeforeClosingTagDigit(CompositeNode node, NodeType type)
    {
        node = HandleNoneClosingTag(node);
        node.Add(new TextNode(TagMatching.NodeTypeMatching[type].mdTag + ' '));
        return node;

    }
    public static CompositeNode? ReplaceCurrentNodeWithTextNode(CompositeNode? node,
        NodeType leftTag = NodeType.Text,
        NodeType rightTag = NodeType.Text)
    {
        if (node == null) return null;
        var textNode = TextNode.DecorateCurrentNodeToTextNode(node, leftTag, rightTag);
        node.Parent.ReplaceNode(node, textNode);
        return node.Parent;
    }
    private static CompositeNode HandleNoneClosingTag(CompositeNode node)
    {
        if (!node.IsBeginInWord) return node;
        if (node.TypeOfNode == NodeType.Italic)
        {
            return HandleNoneClosingTag(ReplaceCurrentNodeWithTextNode(node, NodeType.Italic));
        }
        else if (node.TypeOfNode == NodeType.Bold)
        {
            return HandleNoneClosingTag(ReplaceCurrentNodeWithTextNode(node, NodeType.Bold));
        }
        return node;
    }
}
