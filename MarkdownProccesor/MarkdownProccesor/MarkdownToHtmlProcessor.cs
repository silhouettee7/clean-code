using MarkdownProccesor.Lexer;
using MarkdownProccesor.Renderer;
using MarkdownProccesor.Lexer.Abstract;
using MarkdownProccesor.Renderer.Abstract;
using MarkdownProccesor.Tokens.Abstract;
using System.Text;
using MarkdownProccesor.Tokens.Types;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Handlers;

namespace MarkdownProccesor;

public sealed class MarkdownToHtmlProcessor
{
    private Stack<string> stackOfTags = new Stack<string>();
    private CompositeNode currentNode;
    public string? ConvertToHtml(string markdownText)
    {

        var rootOfHtmlDocument = new DocumentNode();
        currentNode = rootOfHtmlDocument;
        var text = new StringBuilder();
        uint levelOdHeader = 0;
        for (int i = 1; i < markdownText.Length-1; i++)
        {
            var previousSymbol = markdownText[i - 1];
            var currentSymbol = markdownText[i];
            var nextSymbol = markdownText[i + 1];
            if ((previousSymbol == '\n' && currentSymbol == '#') || (currentSymbol == '#' && levelOdHeader > 0))
            {
                levelOdHeader++;
                continue;
            }
            if (previousSymbol == '#' && currentSymbol == ' ' && levelOdHeader <= 6)
            {
                var headerNode = new HeaderNode(currentNode, levelOdHeader);
                currentNode = headerNode;
                levelOdHeader = 0;
                continue;
            }
            if (currentSymbol == '\n' && currentNode.TypeOfNode == NodeType.Header)
            {
                currentNode = currentNode.Parent;
            }

            if (currentSymbol == '_' && nextSymbol == '_' && markdownText[i+2] != ' ')
            {
                if (currentNode.TypeOfNode == NodeType.Bold)
                {
                    currentNode.Parent.Add(ConvertToTextNode(currentNode));
                    currentNode = currentNode.Parent;
                }
                else
                {
                    currentNode.Add(new TextNode(text.ToString()));
                    text.Clear();
                    stackOfTags.Push("__");
                    var boldNode = new BoldNode(currentNode);
                    currentNode = boldNode;
                    i++;
                    continue;
                }
            }
            if (currentSymbol == '_' && nextSymbol == '_' && previousSymbol != ' ')
            {
                if (stackOfTags.Peek() == "__")
                {
                    currentNode.Add(new TextNode(text.ToString()));
                    text.Clear();
                    stackOfTags.Pop();
                    currentNode = currentNode.Parent;
                    i++;
                    continue;
                }
            }

            if (currentSymbol == '_' && previousSymbol != '_' && nextSymbol != ' ') // todo исправить nextSymbol
            {
                if (currentNode.TypeOfNode == NodeType.Italic)
                {
                    currentNode.Parent.Remove(currentNode);
                    currentNode.Parent.Add(ConvertToTextNode(currentNode)); //todo изменить ConvertToTextNode
                    currentNode = currentNode.Parent;
                }
                currentNode.Add(new TextNode(text.ToString()));
                text.Clear();
                stackOfTags.Push("_");
                var italicNode = new ItalicNode(currentNode);
                currentNode = italicNode;
                continue;

            }
            if (currentSymbol == '_' && nextSymbol != '_' && previousSymbol != ' ')
            {
                if (stackOfTags.Peek() == "_")
                {
                    currentNode.Add(new TextNode(text.ToString()));
                    text.Clear();
                    stackOfTags.Pop();
                    currentNode = currentNode.Parent;
                    continue;
                }
            }

            text.Append(currentSymbol);
        }
        
        return rootOfHtmlDocument.Represent();
    }

    private TextNode ConvertToTextNode(CompositeNode node)
    {
        return new TextNode((node.TypeOfNode == NodeType.Italic ? "_": "__") + node.RepresentWithoutTags());
    }

    public string Convert(string markdownText)
    {
        var rootOfHtmlDocument = new DocumentNode();
        var textSplitByLines = markdownText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var textHandler = new TextNodeHandler();
        var boldHandler = new BoldNodeHandler(stackOfTags);
        var italicHandler = new ItalicNodeHandler(stackOfTags);
        var headerHandler = new HeaderNodeHandler();
        headerHandler.Successor = textHandler;
        textHandler.Successor = boldHandler;
        boldHandler.Successor = italicHandler;
        italicHandler.Successor = textHandler;
        foreach(var line in textSplitByLines)
        {
            var textSplitByWords = line.Split(new char[] { '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var processedLine = new ProcessedLineOfWords(textSplitByWords.Select((word) => new ProcessedWord(word)));
            headerHandler.HandleLine(processedLine, rootOfHtmlDocument);
        }
        
        return rootOfHtmlDocument.Represent();
    }
}
