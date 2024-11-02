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
