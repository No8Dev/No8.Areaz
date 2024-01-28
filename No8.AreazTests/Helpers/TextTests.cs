using No8.Areaz.Helpers;

namespace No8.AreazTests.Helpers;

using static TextHelpers;

[TestClass]
public class TextTests
{
    static List<string> EmptyStringList = new();


    [Test]
    public void SimpleStrings_SplitInfoParts()
    {
        Assert.AreEqual(
            "".SplitIntoParts(),
            EmptyStringList);

        Assert.AreEqual(
            "One".SplitIntoParts(),
            new[] { "One" });

        Assert.AreEqual(
            "One Two".SplitIntoParts(),
            new[] { "One", " ", "Two" }
            );
    }

    [Test]
    public void TrailingWhiteSpace_SplitIntoParts()
    {
        Assert.AreEqual(
            " ".SplitIntoParts(),
            EmptyStringList
            );
        Assert.AreEqual(
            "\n ".SplitIntoParts(),
            new[] { "\n" }
            );
    }

    [Test]
    public void MultipleWhiteSpace_SplitIntoParts()
    {
        Assert.AreEqual(
            "One  Two".SplitIntoParts(),
            new[] { "One", "  ", "Two" }
            );

        Assert.AreEqual(
            "One \t\f\r\n Two".SplitIntoParts(),
            new[] { "One", " \t\f", "\n", " ", "Two" }
            );

        Assert.AreEqual(
            "One\n \t\fTwo".SplitIntoParts(),
            new[] { "One", "\n", " \t\f", "Two" }
            );
    }

    [Test]
    public void SimpleStringLessThanMaxLength_SplitIntoLines()
    {
        Assert.AreEqual(
            "123 567".SplitIntoLines(10),
            new[] { "123 567" }
            );

        Assert.AreEqual(
            "123 567890".SplitIntoLines(10),
            new[] { "123 567890" }
            );

        Assert.AreEqual(
            "1234567890".SplitIntoLines(10),
            new[] { "1234567890" }
            );
    }

    [Test]
    public void SimpleStringMoreThanMaxLength_SplitIntoLines()
    {
        Assert.AreEqual(
            "123 5678901".SplitIntoLines(10),
            new[] { "123", "5678901" }
            );

        Assert.AreEqual(
            "12345678901".SplitIntoLines(10),
            new[] { "1234567890", "1" }
            );
    }

    [Test]
    public void MultiLine_SplitIntoLines()
    {
        // Trailing space on first line
        Assert.AreEqual(
            "123 56789 123".SplitIntoLines(10),
            new[] { "123 56789", "123" }
            );

        // Leading space on second line
        Assert.AreEqual(
            "123 567890 23".SplitIntoLines(10),
            new[] { "123 567890", "23" }
            );

        // First word at max length
        Assert.AreEqual(
            "1234567890 23".SplitIntoLines(10),
            new[] { "1234567890", "23" }
            );

        // First line more than max length
        Assert.AreEqual(
            "123 5678901 34".SplitIntoLines(10),
            new[] { "123", "5678901 34" }
            );

        // First word more than max length
        Assert.AreEqual(
            "12345678901 34".SplitIntoLines(10),
            new[] { "1234567890", "1 34" }
            );

        // Second word at max length
        Assert.AreEqual(
            "123 5678901234 67".SplitIntoLines(10),
            new[] { "123", "5678901234", "67" }
            );

        // Second word more than max length
        Assert.AreEqual(
            "123 56789012345 78".SplitIntoLines(10),
            new[] { "123", "5678901234", "5 78" }
            );
    }

    [Test]
    public void MultiLineNewLine_SplitIntoLines()
    {
        Assert.AreEqual(
            "123\n56789 123".SplitIntoLines(10),
            new[] { "123", "56789 123" }
            );
        Assert.AreEqual(
            "123 56789\n123".SplitIntoLines(10),
            new[] { "123 56789", "123" }
            );
        Assert.AreEqual(
            "123 56789 123\n".SplitIntoLines(10),
            new[] { "123 56789", "123" }
            );

        // Leading space on second line
        Assert.AreEqual(
            "123 567890\n23".SplitIntoLines(10),
            new[] { "123 567890", "23" }
            );

        // First word at max length
        Assert.AreEqual(
            "1234567890\n23".SplitIntoLines(10),
            new[] { "1234567890", "23" }
            );

        // First line more than max length
        Assert.AreEqual(
            "123 5678901\n34".SplitIntoLines(10),
            new[] { "123", "5678901", "34" }
            );

        // First word more than max length
        Assert.AreEqual(
            "12345678901\n34".SplitIntoLines(10),
            new[] { "1234567890", "1", "34" }
            );

        // Second word at max length
        Assert.AreEqual(
            "123 5678901234\n67".SplitIntoLines(10),
            new[] { "123", "5678901234", "67" }
            );

        // Second word more than max length
        Assert.AreEqual(
            "123 56789012345\n78".SplitIntoLines(10),
            new[] { "123", "5678901234", "5", "78" }
            );
    }

    [Test]
    public void Puncuation_SplitIntoLines()
    {
        Assert.AreEqual(
            "123, 567".SplitIntoLines(10),
            new[] { "123, 567" }
            );

        Assert.AreEqual(
            "123, 67890".SplitIntoLines(10),
            new[] { "123, 67890" }
            );

        Assert.AreEqual(
            "123456789,".SplitIntoLines(10),
            new[] { "123456789," }
            );

        // Trailing space on first line
        Assert.AreEqual(
            "123 5678. 123".SplitIntoLines(10),
            new[] { "123 5678.", "123" }
            );

        // Leading space on second line
        Assert.AreEqual(
            "123 56789. 23".SplitIntoLines(10),
            new[] { "123 56789.", "23" }
            );

        // First word at max length
        Assert.AreEqual(
            "123456789. 23".SplitIntoLines(10),
            new[] { "123456789.", "23" }
            );

        // First line more than max length
        Assert.AreEqual(
            "123 567890, 34".SplitIntoLines(10),
            new[] { "123", "567890, 34" }
            );

        // First word more than max length
        Assert.AreEqual(
            "1234567890, 34".SplitIntoLines(10),
            new[] { "1234567890", ", 34" }
            );

        // Second word at max length
        Assert.AreEqual(
            "123 567890123, 67".SplitIntoLines(10),
            new[] { "123", "567890123,", "67" }
            );

        // Second word more than max length
        Assert.AreEqual(
            "123 5678901234, 78".SplitIntoLines(10),
            new[] { "123", "5678901234", ", 78" }
            );
    }


    [Test]
    public void SimpleStringLessThanMaxLength_SplitIntoLines_MaxHeight()
    {
        Assert.AreEqual(
            "123 567".SplitIntoLines(10, 1),
            new[] { "123 567" }
            );

        Assert.AreEqual(
            "123 567890".SplitIntoLines(10, 1),
            new[] { "123 567890" }
            );

        Assert.AreEqual(
            "1234567890".SplitIntoLines(10, 1),
            new[] { "1234567890" }
            );

        Assert.AreEqual(
            "12345678901".SplitIntoLines(10, 1),
            new[] { "12345678.." }
            );
    }



    // Single line scenarios
    // ----*----*
    // abc.dev                 less than max width
    // acbdef.ghi              max width
    // abcdefghij              max width one word
    // abc.defghij             more than max width
    // abcdefghijk             more than max width in one word

    // Multi line scenarios
    // ----*----*----*----*
    // acbdef.ghi.abc          max width
    // abcdefghij.abc          max width one word
    // abc.defghij.abc         more than max width
    // abcdefghijk.abc         more than max width in one word

    // abcdef.ghij.abc         second line less than max width
    // acbde.ghi.acbdef.ghi    second line max width
    // abcdefghij.abcdefghij   second line max width one word
    // abc.defgh.abc.defghijk  second line more than max width
    // abcdefghijk.abcdefghijk second line more than max width in one word

    // Can add new line (maxHeight)

    // if truncate
    // $"{Text.Substring(start, (int)max - 2)}..";

    // White Space = Backspace (8 \b), Horz tab (9 \t), Line feed (10 \n), Vert Tab (11 \v), Form feed (12, \f), Carriage return (13, \r), Space(20 ' '), 

    // what about 
    // - punculation (,.-)
    // - justification of words
    // - lots of white space
    // - trim word,
    // - empty line,
    // - Truncate big words, or split at max width
    // - trim start, trim end
    // - indent subsequent lines


}
