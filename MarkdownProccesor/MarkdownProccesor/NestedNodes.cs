
using MarkdownProccesor.Tokens;

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