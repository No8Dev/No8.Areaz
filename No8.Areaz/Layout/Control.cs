using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public abstract class Control : IControl
{
    public string Name { get; set; }

    protected Control(string? name = null, SizeNumber? sizeRequested = null)
    {
        Name = name ?? string.Empty;
    }

    public abstract ILayoutManager? LayoutManager();
    public abstract bool ValidGuide(ILayoutGuide? guide);

    public virtual void PaintIn(Canvas canvas, Rectangle rect) { }
    public virtual void PaintOut(Canvas canvas, Rectangle rect) { }

    public override string ToString() => BuildString(new StringBuilder()).ToString();

    protected virtual StringBuilder BuildString(StringBuilder? sb = null)
    {
        sb ??= new();
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Name)) sb.Append($" [{Name}]");
        //if (SizeRequested is not null) sb.Append($" Size{SizeRequested.Value}");
        return sb;
    }
}