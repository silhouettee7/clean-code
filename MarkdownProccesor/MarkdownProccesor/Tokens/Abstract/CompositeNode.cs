using System.Linq;
using System.Text;

namespace MarkdownProccesor.Tokens.Abstract;

public abstract class CompositeNode: INode, IParent
{
    protected List<INode> _childrenNode = new List<INode>();
    public CompositeNode? Parent { get; }
    public abstract NodeType TypeOfNode { get; }
    public abstract string? Value { get; }
    public virtual string? Represent()
    {
        return $"<{Value}>{RepresentAllChildrenNodes()}</{Value}>";
    }
    public string? RepresentWithoutTags()
    {
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
    protected string? RepresentAllChildrenNodes()
    {
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

