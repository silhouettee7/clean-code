using MarkdownProccesor.Lexer.Abstract;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Lexer;

internal class MarkdownLexer : ILexer<List<CompositeNode>>
{
    public List<CompositeNode> Tokenize(string markdownText)
    {
        throw new NotImplementedException();
    }
}