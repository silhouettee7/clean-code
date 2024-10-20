
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor;

public static class NestedNodes
{
    public static List<(NodeType typeOfNode, List<INode> listOfNode)> NestedListsOfNodes { get; } = 
        new List<(NodeType typeOfNode, List<INode> listOfNode)> () { (NodeType.Document, new List<INode> ())};
}