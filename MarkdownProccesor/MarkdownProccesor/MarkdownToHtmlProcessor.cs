
using MarkdownProccesor.Tokens.Types;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Handlers;

namespace MarkdownProccesor;

public sealed class MarkdownToHtmlProcessor
{
    public string ConvertToHtml(string markdownText)
    {
        var textSplitByLines = markdownText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var textHandler = new TextNodeHandler();
        var boldHandler = new BoldNodeHandler();
        var italicHandler = new ItalicNodeHandler();
        var headerHandler = new HeaderNodeHandler();
        var handwritinHandler = new AllHandwritingHandler();
        headerHandler.Successor = handwritinHandler;
        handwritinHandler.Successor = textHandler;
        textHandler.Successor = boldHandler;
        boldHandler.Successor = italicHandler;
        italicHandler.Successor = textHandler;
        var mdTextHandler = new MarkdownTextHandler(headerHandler);
        
        return mdTextHandler.HandleMdTextLines(textSplitByLines);
    }
}
