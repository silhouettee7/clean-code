using MarkdownProccesor;

namespace MarkdownTests;

public class MarkdownProcessorTest
{
    private readonly MarkdownToHtmlProcessor mdProcessor = new();

    [Fact]
    public void ConvertToHtmlShouldBeHeaderTwoLevel()
    {
        //Arrange
        string mdTextInput = "## Заголовок ";
        string htmlTextExpected = "<html><h2>Заголовок </h2></html>";
        //Act
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        //Assert
        Assert.Equal(htmlTextExpected, result);
    }
    [Fact]
    public void ConvertToHtmlShouldBeItalic()
    {
        string mdTextInput = "_text_";
        string htmlTextExpected = "<html><em>text</em></html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
    [Fact]
    public void ConvertToHtmlShouldBeBold()
    {
        string mdTextInput = "__text__";
        string htmlTextExpected = "<html><strong>text</strong></html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }

    [Fact]
    public void ConvertToHtmlShouldBeItalicBeginOfWord()
    {
        string mdTextInput = "_te_xt";
        string htmlTextExpected = "<html><em>te</em>xt </html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
    [Fact]
    public void ConvertToHtmlShouldBeItalicEndOfWord()
    {
        string mdTextInput = "te_xt_";
        string htmlTextExpected = "<html>te<em>xt</em></html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
    [Fact]
    public void ConvertToHtmlShouldBeItalicMiddleOfWord()
    {
        string mdTextInput = "t_ex_t";
        string htmlTextExpected = "<html>t<em>ex</em>t </html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
    [Fact]
    public void ConvertToHtmlShouldBeBoldBeginOfWord()
    {
        string mdTextInput = "__te__xt";
        string htmlTextExpected = "<html><strong>te</strong>xt </html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
    [Fact]
    public void ConvertToHtmlShouldBeBoldEndOfWord()
    {
        string mdTextInput = "te__xt__";
        string htmlTextExpected = "<html>te<strong>xt</strong></html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
    [Fact]
    public void ConvertToHtmlShouldBeBoldMiddleOfWord()
    {
        string mdTextInput = "t__ex__t";
        string htmlTextExpected = "<html>t<strong>ex</strong>t </html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }

    [Fact]
    public void ConvertToHtmlShouldBeStayHandwritingInDifferentWords()
    {
        string mdTextInput = "t_ext tex_t";
        string htmlTextExpected = "<html>t_ext tex_t </html>";
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }

    [Theory]
    [InlineData("__text _text_ text__", "<html><strong>text <em>text</em>text</strong></html>")]
    [InlineData("__t_ex_t__", "<html><strong>t<em>ex</em>t</strong></html>")]
    public void ItalicInBoldShouldBeCorrect(string mdTextInput, string htmlTextExpected)
    {
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }

    [Theory]
    [InlineData("_text __text__ text_", "<html><em>text __text__text</em></html>")]
    [InlineData("_t__ex__t_", "<html><em>t__ex__t</em></html>")]
    public void BoldInItalicShouldBeWrong(string mdTextInput, string htmlTextExpected)
    {
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }

    [Theory]
    [InlineData("_text __text_ tex__", "<html>_text __text_tex__</html>")]
    [InlineData("__text _text__ tex_", "<html>__text _text__tex_</html>")]
    [InlineData("__t_ex__t_", "<html>__t_ex__t_</html>")]
    [InlineData("_t__ex_t__", "<html>_t__ex_t__</html>")]
    public void OverlappingTagsShouldBeStayHandwriting(string mdTextInput, string htmlTextExpected)
    {
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
    
    [Theory]
    [InlineData("__text_", "<html>__text_</html>")]
    [InlineData("_text__", "<html>_text__</html>")]
    public void UnpairedTagsShouldBeStayHandwriting(string mdTextInput, string htmlTextExpected)
    {
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }

    [Theory]
    [InlineData("____", "<html>____</html>")]
    [InlineData("__", "<html>__</html>")]
    [InlineData("___", "<html>___</html>")]
    public void EmptyTagsShouldBeStayHandwriting(string mdTextInput, string htmlTextExpected)
    {
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }

    [Theory]
    [InlineData("_text _text_", "<html>_text <em>text</em></html>")]
    [InlineData("__text __text__", "<html>__text <strong>text</strong></html>")]
    [InlineData("__tex_t__", "<html><strong>tex_t</strong></html>")]
    [InlineData("_tex__t_", "<html><em>tex__t</em></html>")]
    public void PairTagWithoutClosingShouldBeStayHandwriting(string mdTextInput, string htmlTextExpected)
    {
        string result = mdProcessor.ConvertToHtml(mdTextInput);
        Assert.Equal(htmlTextExpected, result);
    }
   
}
