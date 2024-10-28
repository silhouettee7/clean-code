

using MarkdownProccesor.Tokens;
using MarkdownProccesor.Tokens.Abstract;
using MarkdownProccesor.Tokens.Types;

namespace MarkdownProccesor.Handlers.Abstract;
public abstract class ItalicWithBoldTagsHandler: FormattingTagsHandler
{
    public NodeType OppositeTag { get => TagType == NodeType.Italic ? NodeType.Bold : NodeType.Italic; }
    public override CompositeNode HandleEndWord()
    {
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
            _currentNode = _currentNode.Parent;
        }
        return _currentNode;
    }
    public override CompositeNode HandleInsideWord()
    {
        _word.AddCurrentIndexValue();
        if (_currentNode.TypeOfNode == TagType)
        {
            HandleIntersectTags();
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
        if (sameNodeParent.TypeOfNode == TagType) return ReplaceCurrentNodeWithTextNode(_currentNode, OppositeTag).Parent;
        return ReplaceCurrentNodeWithTextNode(_currentNode, OppositeTag, TagType);
    }
    public virtual CompositeNode HandleInsideWordIfTagType()
    {
        return Successor.HandleWord(_word, _currentNode.Parent!);
    }
    protected void HandleIntersectTags()
    {
        if (_currentNode.TypeOfNode != NodeType.Italic && _currentNode.TypeOfNode != NodeType.Bold)
        {
            var twoLast = _currentNode.GetTwoLastChildren();
            if (twoLast.first == null) return;
            if (twoLast.first.TypeOfNode == OppositeTag)
            {
                _currentNode.Remove(twoLast.first);
                _currentNode.Remove(twoLast.second);
                _currentNode.Add(TextNode.DecorateCurrentNodeToTextNode(twoLast.first, OppositeTag, OppositeTag));
                _currentNode.Add(twoLast.second);
            }
        }
    }

}
