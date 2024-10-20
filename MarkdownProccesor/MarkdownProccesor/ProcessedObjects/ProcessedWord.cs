
namespace MarkdownProccesor.ProcessedObjects;

public class ProcessedWord
{
    private int _currentIndex;
    public string? Value { get; }
    public char CurrentChar { get => IsProcessed ? default : Value[_currentIndex]; }
    public char NextChar { get => _currentIndex + 1 >= Value.Length ? default : Value[_currentIndex + 1]; }
    public bool IsProcessed { get => _currentIndex >= Value.Length; }
    public bool IsBeginning { get => _currentIndex == 0; }
    public bool IsEndForBold { get => _currentIndex == Value.Length - 2; }
    public bool IsEndForItalic { get => _currentIndex == Value.Length - 1; }
    public void AddCurrentIndexValue(int value)
    {
        _currentIndex += value;
    }
    public ProcessedWord(string value)
    {
        Value = value;
    }
}
