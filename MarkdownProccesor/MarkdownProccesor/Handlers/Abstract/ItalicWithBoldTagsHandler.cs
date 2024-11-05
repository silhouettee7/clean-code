using MarkdownProccesor.Handlers.Tools;
using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Handlers.Abstract;
public abstract class ItalicWithBoldTagsHandler: FormattingTagsHandler
{
    public NodeType OppositeTag { get => TagType == NodeType.Italic ? NodeType.Bold : NodeType.Italic; }
    public override CompositeNode HandleEndWord()
    {
        if (char.IsDigit(_word.Previous)) 
        {
            return HandleNodesHelper.HandleEndWordIfBeforeClosingTagDigit(_currentNode, TagType);
        }

        if (_currentNode.TypeOfNode == TagType)
        {
            _currentNode = HandleEndWordIfTagType();
        }
        else if (_currentNode.TypeOfNode == OppositeTag)
        {
            _currentNode = HandleEndNodeIfOppositeTag();
        }
        else
        {
            HandleIntersectTags();
            _currentNode.Add(new TextNode(TagMatching.NodeTypeMatching[TagType].mdTag));
        }
        _currentNode.Add(new TextNode(" "));
        return _currentNode;
    }
    public override CompositeNode HandleInsideWord()
    {
        if (char.IsDigit(_word.Next) || char.IsDigit(_word.Previous))
        {
            _currentNode.Add(new TextNode(TagMatching.NodeTypeMatching[TagType].mdTag));
            _word.AddCurrentIndexValue();
            return Successor.HandleWord(_word, _currentNode);
        }
        _word.AddCurrentIndexValue();
        if (_currentNode.TypeOfNode == TagType)
        {
            _currentNode = HandleInsideWordIfTagType();
        }
        else if (_currentNode.TypeOfNode == OppositeTag && _currentNode.IsBeginInWord)
        {
            _currentNode = Successor.HandleWord(_word, HandleEndNodeIfOppositeTag());
        }
        else
        {
            _currentNode = GetCreatedInWordNewNode();
        }
        return _currentNode;
    }
    public virtual CompositeNode HandleEndWordIfTagType()
    {
        return _currentNode.Parent!;
    }
    public virtual CompositeNode HandleEndNodeIfOppositeTag()
    {
        var sameNodeParent = _currentNode.Parent;
        if (sameNodeParent.TypeOfNode == TagType)
        {
            sameNodeParent.IsContainOppositeTag = true;
            return HandleNodesHelper.ReplaceCurrentNodeWithTextNode(_currentNode, OppositeTag).Parent;
        }            
        return HandleNodesHelper.ReplaceCurrentNodeWithTextNode(_currentNode, OppositeTag, TagType);
    }
    public virtual CompositeNode HandleInsideWordIfTagType()
    {
        return Successor.HandleWord(_word, _currentNode.Parent!);
    }
    protected void HandleIntersectTags()
    {
        var lastCompositeNode = _currentNode.GetLastCompositeNode();
        if (lastCompositeNode == null) return;
        if (lastCompositeNode.TypeOfNode == OppositeTag && lastCompositeNode.IsContainOppositeTag)
        {
            _currentNode.ReplaceNode(lastCompositeNode,
            TextNode.DecorateCurrentNodeToTextNode(lastCompositeNode, OppositeTag, OppositeTag));
        }
    }

}
