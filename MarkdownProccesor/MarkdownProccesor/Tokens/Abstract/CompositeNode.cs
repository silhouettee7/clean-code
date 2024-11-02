﻿using MarkdownProccesor.Tokens.Types;
using System.Linq;
using System.Text;

namespace MarkdownProccesor.Tokens.Abstract;

public abstract class CompositeNode: INode
{
    protected List<INode> _childrenNode = new List<INode>();
    public CompositeNode? Parent { get; set; }
    public int PositionInParentChildren { get; }
    public int CountOfChildren { get => _childrenNode.Count; }
    public abstract NodeType TypeOfNode { get; }
    public abstract string? Value { get; }
    public bool IsBeginInWord { get; set; }
    public bool IsEmpty { get => _childrenNode.Count == 0;  }
    public bool IsFinished { get; private set; }
    public bool IsContainOppositeTag { get; set; }
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
    public void ReplaceNode(CompositeNode current, INode replacement)
    {
        _childrenNode[current.PositionInParentChildren] = replacement;
    }
    public CompositeNode? GetLastCompositeNode()
    {
        return _childrenNode.OfType<CompositeNode>().LastOrDefault();
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
        PositionInParentChildren = Parent.CountOfChildren - 1;
    }
}

