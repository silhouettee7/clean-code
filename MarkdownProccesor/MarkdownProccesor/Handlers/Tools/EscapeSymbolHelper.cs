
using MarkdownProccesor.ProcessedObjects;
using MarkdownProccesor.Tokens;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MarkdownProccesor.Handlers.Tools;
public static class EscapeSymbolHelper
{
    private static bool _isEscape = true;
    public static string HandleEscapeSymbols(ProcessedWord word)
    {
        var text = new StringBuilder();
        string specialSymbol = string.Empty;
        while (word.Current == "\\")
        {
            if (_isEscape)
            {
                text.Append("\\");
                _isEscape = false;
            }
            else
            {
                _isEscape = true;
            }
            word.AddCurrentIndexValue();
        }
        if (IsEscapeSymbolStandsBeforeSpecial(ref specialSymbol, word, NodeType.Italic, NodeType.Bold))
        {
            text.Remove(text.Length-1,1);
            text.Append(specialSymbol);
        }
        _isEscape = true;
        return text.ToString();
    }
    private static bool IsEscapeSymbolStandsBeforeSpecial(ref string special,
        ProcessedWord word, 
        params NodeType[] specialSymbols)
    {
        if (_isEscape) return false; 
        bool result = false;
        foreach(var type in specialSymbols)
        {
            word.ContextNode = type;
            result = word.Current == TagMatching.NodeTypeMatching[type].mdTag;
            if (result)
            {
                special = word.Current;
                word.AddCurrentIndexValue();
                break;
            }
        }
        word.ContextNode = NodeType.Text;
        return result;
    }
}
