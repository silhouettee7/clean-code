using System.Linq;
using System.Text;

namespace MarkdownProccesor.Tokens.Abstract;

public abstract class CompositeNode: INode
{
    protected List<INode> _childrenNode = new List<INode>();
    public CompositeNode? Parent { get; }
    public abstract NodeType TypeOfNode { get; }
    public abstract string? Value { get; }
    public bool IsBeginInWord { get; set; }
    public bool IsEmpty { get => _childrenNode.Count == 0;  }
    public bool IsFinished { get; private set; }
    public virtual string? Represent()
    {
        IsFinished = true;
        return $"<{Value}>{RepresentAllChildrenNodes()}</{Value}>";
    }
    public string? RepresentWithoutTags()
    {
        IsFinished = false;
        return RepresentAllChildrenNodes();
    }
    public void Add(INode child)
    {
        _childrenNode.Add(child);
    }
    public void Remove(INode child)
    {
        _childrenNode.Remove(child);
    }
    public (CompositeNode? first, INode? second) GetTwoLastChildren()
    {
        if (_childrenNode.Count < 2) return (null, null);
        var twoLast = _childrenNode.TakeLast(2);
        return (twoLast.First() as CompositeNode, twoLast.Last());
    }
    protected string? RepresentAllChildrenNodes()
    {
        if (IsEmpty) return "";
        return _childrenNode.Select(node => node.Represent())
            .Aggregate((s1, s2) => s1+s2);
    }
    public CompositeNode()
    {
        
    }
    public CompositeNode(CompositeNode parent)
    {
        Parent = parent;
        Parent.Add(this);
    }
}

