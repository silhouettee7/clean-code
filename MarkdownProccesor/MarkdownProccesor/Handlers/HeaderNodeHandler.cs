﻿
using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Handlers;

internal class HeaderNodeHandler : INodeHandler
{
    public INodeHandler? Successor { get; set; }

    public CompositeNode HandleWord(ProcessedWord word, CompositeNode currentNode)
    {
        if (word.Value.All((element) => element == '#') && word.IsFirst)
        {
            var headerNode = new HeaderNode(currentNode, (uint)word.Value.Length);
            return headerNode;
        }
        return Successor.HandleWord(word, currentNode);
    }
}
