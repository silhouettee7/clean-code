using MarkdownProccesor.Renderer.Abstract;
using MarkdownProccesor.Tokens.Abstract;
using System.Text;

namespace MarkdownProccesor.Renderer;

internal sealed class MarkdownToHtmlRenderer : IMarkdownRenderer<List<CompositeNode>>
{
    public string RenderMarkdwonText(List<CompositeNode> tokens)
    {
        throw new NotImplementedException();
    }
}
