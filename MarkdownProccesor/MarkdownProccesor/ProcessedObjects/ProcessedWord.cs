
using MarkdownProccesor.Tokens;
using static MarkdownProccesor.NestedNodes;
namespace MarkdownProccesor.ProcessedObjects;

public class ProcessedWord
{
    private int _currentIndex;
    private string value;
    public string? Value { get => value.Substring(_currentIndex) ; }
    public string Current
    {
        get
        {
            if (ContextNode == NodeType.Italic || ContextNode == NodeType.Text)
            {
                return IsProcessed ? default : value.Substring(_currentIndex,1);
            }
            else if (ContextNode == NodeType.Bold)
            {
                return _currentIndex + 1 >= value.Length ? default : value.Substring(_currentIndex, 2);
            }
            return string.Empty;
        }
    }
    public char Previous { get => _currentIndex > 0 ? value[_currentIndex-1]: default; }
    public char CurrentAfterAddIndex { get => _currentIndex < value.Length ? value[_currentIndex] : default; }
    public char Next
    {
        get
        {
            if (ContextNode == NodeType.Italic || ContextNode == NodeType.Text)
            {
                return _currentIndex + 1 >= value.Length ? default : value[_currentIndex+1];
            }
            else if (ContextNode == NodeType.Bold)
            {
                return _currentIndex + 2 >= value.Length ? default : value[_currentIndex + 2];
            }
            return default;
        }
    }
    public bool IsFirst { get; set; }
    public bool IsProcessed { get => _currentIndex >= value.Length; }
    public bool IsBeginning { get => _currentIndex == 0; }
    public bool IsEnd
    {
        get => _currentIndex == value.Length - TagMatching.NodeTypeMatching[ContextNode].length;
    }
    public NodeType ContextNode { get; set; } = NodeType.Text;
    public void AddCurrentIndexValue()
    {
        _currentIndex += TagMatching.NodeTypeMatching[ContextNode].length;
    }
    public ProcessedWord(string value)
    {
        this.value = value;
    }
}
