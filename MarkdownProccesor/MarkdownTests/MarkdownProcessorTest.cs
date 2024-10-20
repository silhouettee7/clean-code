using MarkdownProccesor;

namespace MarkdownTests;

public class MarkdownProcessorTest{
    [Fact]

    public void ConvertToHtmlTagShouldBeMatchHeaderTwoLevel()
    {
        //Arrange
        var mdProcessor = new MarkdownToHtmlProcessor();
        string mdTextInput = "\n## ��������� _textboldtext _word_ \n";
        string htmlTextExpected = "<html><h2>��������� _textboldtext <em>word</em> </h2></html>";
        //Act
        string result = mdProcessor.Convert(mdTextInput);
        //Assert
        Assert.Equal(htmlTextExpected, result);
    }
}