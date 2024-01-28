using No8.Areaz.CommandLine;
using No8.AreazTests.Models;

namespace No8.AreazTests;

public class CommandLineTests
{
    private readonly ArgsParser _parser = new ArgsParser()
                                         .SetIncludeEnvironmentVariables()
                                         .AddCommand<OptionArgs>(isDefault: true);

    [Test]
    public void TestCaseInsensitive()
    {
        var parser = new ArgsParser()
                    .SetIncludeEnvironmentVariables()
                    .SetCaseSensitive(false)
                    .AddCommand<OptionArgs>(isDefault: true);

        // Case insensitive
        var obj = (OptionArgs)parser.Parse("--Flag=true --Int=0 --Float=1 --String=what --Enum=first --List=yes")!;

        Assert.Multiple(() =>
        {
            Assert.True(obj.Flag);
            Assert.AreEqual(0, obj.IntValue);
            Assert.AreEqual(1f, obj.FloatValue);
            Assert.AreEqual("what", obj.StringValue);
            Assert.AreEqual(OptionArgsMode.First, obj.EnumValue);
            Assert.AreEqual("yes", obj.ListValue![0]);
        });
    }

    [Test]
    public void TestNegatives()
    {
        // Case sensitive
        Assert.Throws<ArgumentException>(() => _parser.Parse("--Flag=true"), "Parameter [Flag] has not been defined");
        Assert.Throws<ArgumentException>(() => _parser.Parse("--Int=0"), "Parameter [Int] has not been defined");
        Assert.Throws<ArgumentException>(() => _parser.Parse("--Float=1"), "Parameter [Float] has not been defined");
        Assert.Throws<ArgumentException>(() => _parser.Parse("--String=what"), "Parameter [String] has not been defined");
        Assert.Throws<ArgumentException>(() => _parser.Parse("--Enum=First"), "Parameter [Enum] has not been defined");
        Assert.Throws<ArgumentException>( () => _parser.Parse("--enum=first"), "Requested value 'first' was not found.");
        Assert.Throws<ArgumentException>(() => _parser.Parse("--List=no"), "Parameter [List] has not been defined");
    }

    [Test]
    public void TestBoolean()
    {
        var obj = (OptionArgs)_parser.Parse($"--flag=true")!;
        Assert.True(obj.Flag);

        obj = (OptionArgs)_parser.Parse($"--f=False")!;
        Assert.False(obj.Flag);
    }

    [Test]
    public void TestInt()
    {
        var obj = (OptionArgs)_parser.Parse("--int=1")!;
        Assert.AreEqual(1, obj.IntValue);

        obj      = (OptionArgs)_parser.Parse("--i=2")!;
        Assert.AreEqual(2, obj.IntValue);

        obj      = (OptionArgs)_parser.Parse($"--int16={short.MinValue}")!;
        Assert.AreEqual(short.MinValue, obj.Int16Value);

        obj      = (OptionArgs)_parser.Parse($"--int32={Int32.MaxValue}")!;
        Assert.AreEqual(Int32.MaxValue, obj.Int32Value);

        obj      = (OptionArgs)_parser.Parse($"--int64={Int64.MaxValue}")!;
        Assert.AreEqual(Int64.MaxValue, obj.Int64Value);
    }

    [Test]
    public void TestNumbers()
    {
        var obj = (OptionArgs)_parser.Parse($"--float={1.0f}")!;
        Assert.AreEqual(1.0f, obj.FloatValue);

        obj      = (OptionArgs)_parser.Parse($"--double={1.1d}")!;
        Assert.AreEqual(1.1d, obj.DoubleValue);

        obj      = (OptionArgs)_parser.Parse($"--decimal={1.2m}")!;
        Assert.AreEqual(1.2m, obj.DecimalValue);
    }


    [Test]
    public void TestString()
    {
        var obj = (OptionArgs)_parser.Parse($"--string=1")!;
        Assert.AreEqual("1", obj.StringValue);

        obj      = (OptionArgs)_parser.Parse($"--s=2")!;
        Assert.AreEqual("2", obj.StringValue);
    }

    [Test]
    public void TestDateTime()
    {
        var dt = DateTime.Now;

        var obj = (OptionArgs)_parser.Parse($"--date={dt:O}")!;
        Assert.AreEqual(dt, obj.DateValue);
    }

    [Test]
    public void TestEnum()
    {
        var obj = (OptionArgs)_parser.Parse($"--enum={OptionArgsMode.First}")!;
        Assert.AreEqual(OptionArgsMode.First, obj.EnumValue);

        obj      = (OptionArgs)_parser.Parse($"--e=Second")!;
        Assert.AreEqual(OptionArgsMode.Second, obj.EnumValue);
    }

    [Test]
    public void TestList()
    {
        var obj = (OptionArgs)_parser.Parse($"--list=First")!;
        Assert.AreEqual("First", obj.ListValue![0]);

        obj      = (OptionArgs)_parser.Parse($"--list=First --l=Second")!;
        Assert.AreEqual("Second", obj.ListValue![1]);
    }

    [Test]
    public void TestCommandLines()
    {
        var obj = (OptionArgs)_parser.Parse(
            new List<string>
            {
                "-flag",
                "--int",
                "1",
                "/int16",
                "2",
                "-int32=3",
                "--int64:4",
                "/enum=Second"
            }, out _)!;

        Assert.True(obj.Flag);
        Assert.AreEqual(1, obj.IntValue);
        Assert.AreEqual(2, obj.Int16Value);
        Assert.AreEqual(3, obj.Int32Value);
        Assert.AreEqual(4, obj.Int64Value);
        Assert.AreEqual(OptionArgsMode.Second, obj.EnumValue);
    }

    [Test]
    public void TestEmptyCommandLine()
    {
        _parser.Parse("");
    }

    [Test]
    public void TextExtras()
    {
        var obj = (OptionArgs)_parser.Parse($"cmd")!;
        Assert.Null(obj.ListValue);

        _ = (OptionArgs)_parser.Parse($"ArgsOptions Extra Text", out var extras)!;
        Assert.AreEqual(2, extras!.Count);

        _ = (OptionArgs)_parser.Parse($"doit Extra Text", out extras)!;
        Assert.AreEqual(3, extras!.Count);
    }

    [Test]
    public void TextHelp()
    {
        var obj = _parser.Parse($"--?")!;
        if (obj is not HelpCommand)
            Assert.True(false, "No help");
    }

}