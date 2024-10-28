using MarkdownProccesor;
using MarkdownProccesor.Handlers.Abstract;
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

public abstract class FormattingTagsHandler : INodeHandler
{ 
    protected ProcessedWord _word;
    protected CompositeNode _currentNode;
    public INodeHandler Successor { get ; set; }
    public abstract NodeType TagType { get; }
    public abstract CompositeNodeFactory Factory { get; }
    public CompositeNode HandleWord(ProcessedWord word, CompositeNode currentNode)
    {
        word.ContextNode = TagType;
        if (word.Current != TagMatching.NodeTypeMatching[TagType].mdTag) return Successor.HandleWord(word, currentNode);
        _word = word;
        _currentNode = currentNode;
        if (_word.IsBeginning)
        {
            _currentNode = HandleBeginWord();
        }
        else if (_word.IsEnd)
        {
            _currentNode = HandleEndWord();
        }
        else
        {
            _currentNode = HandleInsideWord();
        }
        return _currentNode;
    }
    public virtual CompositeNode HandleBeginWord()
    {
        var node = Factory.CreateCompositeNode(_currentNode);
        _word.AddCurrentIndexValue();
        var pair = GetSameNodeParentAndHisChild(node);
        if (pair.parent != null)
        {
            pair.parent.Remove(pair.childOfParent);
            _currentNode = ReplaceCurrentNodeWithTextNode(pair.parent, TagType);
            _currentNode.Add(pair.childOfParent);
        }
        return Successor.HandleWord(_word, node);
    }
    public abstract CompositeNode HandleEndWord();
    public abstract CompositeNode HandleInsideWord();   
    protected (CompositeNode? parent, CompositeNode childOfParent) GetSameNodeParentAndHisChild(CompositeNode prevNode)
    {
        var tempNode = prevNode.Parent;
        while (tempNode != null)
        {
            if (tempNode.TypeOfNode == TagType) break;
            prevNode = tempNode;
            tempNode = tempNode.Parent;
        }
        return (tempNode,prevNode);
    }
    protected CompositeNode? ReplaceCurrentNodeWithTextNode(CompositeNode? node,
        NodeType leftTag = NodeType.Text,
        NodeType rightTag = NodeType.Text)
    {
        if (node == null) return null;
        var textNode = TextNode.DecorateCurrentNodeToTextNode(node, leftTag, rightTag);
        node.Parent.Remove(node);
        node.Parent.Add(textNode);
        return node.Parent;
    }
    protected CompositeNode GetCreatedInWordNewNode()
    {
        var node = Factory.CreateCompositeNode(_currentNode);
        node.IsBeginInWord = true;
        return Successor.HandleWord(_word, node);
    }
}
