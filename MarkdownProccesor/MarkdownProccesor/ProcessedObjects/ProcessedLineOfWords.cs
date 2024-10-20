
namespace MarkdownProccesor.ProcessedObjects;

public class ProcessedLineOfWords
{
    private readonly IEnumerable<ProcessedWord> _words;
    private readonly IEnumerator<ProcessedWord> _wordsEnumerator;
    public ProcessedLineOfWords(IEnumerable<ProcessedWord> words)
    {
        _words = words;
        _wordsEnumerator = _words.GetEnumerator();
        _wordsEnumerator.MoveNext();
    }
    public ProcessedWord Current { get => _wordsEnumerator.Current; }
    public ProcessedWord? FirstWord {get => _words.FirstOrDefault();}
    public bool TryGoToNextWord()
    {
        bool success = _wordsEnumerator.MoveNext();
        return success;
    }
}
