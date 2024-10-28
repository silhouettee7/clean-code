
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor;

public static class TagMatching
{
    public static Dictionary<NodeType, (string htmlTag, string mdTag, int length)> NodeTypeMatching { get; } = new()
    {
        {NodeType.Italic, ("em","_",1) },
        {NodeType.Bold, ("strong","__",2) },
        {NodeType.Text, ("","",1) },
    };
    
}
public static class NestedNodes
{
    public static List<TemporaryCompositeNode> NestedListsOfNodes { get; } = 
        new List<TemporaryCompositeNode> () { new TemporaryCompositeNode() { typeOfNode = NodeType.Document } };
}

public class TemporaryCompositeNode
{
    public NodeType typeOfNode { get; set; }
    public List<INode> ListOfNode { get; } = new();

}