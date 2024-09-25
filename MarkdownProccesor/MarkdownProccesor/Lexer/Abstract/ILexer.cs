using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Lexer.Abstract;

public interface ILexer<out T> where T : ICollection<Token>
{
    T Tokenize(string markdownText);
}
