using MarkdownProccesor.Tokens.Abstract;

namespace MarkdownProccesor.Renderer.Abstract;

public interface IMarkdownRenderer<in T> where T : ICollection<Token>
{
    string RenderMarkdwonText(T tokens);
}