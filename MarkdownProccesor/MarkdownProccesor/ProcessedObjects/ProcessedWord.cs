
using MarkdownProccesor.Tokens;
namespace MarkdownProccesor.ProcessedObjects;

public class ProcessedWord
{
    private int _currentIndex;
    public string? Value { get ; }
    public string Current
    {
        get
        {
            if (ContextNode == NodeType.Italic || ContextNode == NodeType.Text)
            {
                return IsProcessed ? default : Value.Substring(_currentIndex,1);
            }
            else if (ContextNode == NodeType.Bold)
            {
                return _currentIndex + 1 >= Value.Length ? default : Value.Substring(_currentIndex, 2);
            }
            return string.Empty;
        }
    }
    public char Previous { get => _currentIndex > 0 ? Value[_currentIndex-1]: default; }
    public char Next
    {
        get
        {
            if (ContextNode == NodeType.Italic || ContextNode == NodeType.Text)
            {
                return _currentIndex + 1 >= Value.Length ? default : Value[_currentIndex+1];
            }
            else if (ContextNode == NodeType.Bold)
            {
                return _currentIndex + 2 >= Value.Length ? default : Value[_currentIndex + 2];
            }
            return default;
        }
    }
    public bool IsFirst { get; set; }
    public bool IsProcessed { get => _currentIndex >= Value.Length; }
    public bool IsBeginning { get => _currentIndex == 0; }
    public bool IsEnd
    {
        get => _currentIndex == Value.Length - TagMatching.NodeTypeMatching[ContextNode].length;
    }
    public NodeType ContextNode { get; set; } = NodeType.Text;
    public void AddCurrentIndexValue()
    {
        _currentIndex += TagMatching.NodeTypeMatching[ContextNode].length;
    }
    public ProcessedWord(string value)
    {
        Value = value;
    }
}
