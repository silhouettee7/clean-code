
using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Factories;

namespace MarkdownProccesor.Handlers;  
public class BoldNodeHandler : ItalicWithBoldTagsHandler
{
    public override NodeType TagType => NodeType.Bold;
    public override CompositeNodeFactory Factory => new BoldNodeFactory();
}
