using MarkdownProccesor.Lexer;
using MarkdownProccesor.Renderer;
using MarkdownProccesor.Lexer.Abstract;
using MarkdownProccesor.Renderer.Abstract;
using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor;
public sealed class MarkdownToHtmlProcessor
{
    public string ConvertToHtml<T>(string markdwontext, ILexer<T> lexer, IMarkdownRenderer<T> renderer) 
        where T : ICollection<Token>
    {
        return renderer.RenderMarkdwonText(lexer.Tokenize(markdwontext));
    }
    public string ConvertToHtml(string markdwontext)
    {
        return ConvertToHtml(markdwontext, new MarkdownLexer(), new MarkdownToHtmlRenderer());
    }
}
