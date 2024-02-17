namespace No8.Areaz.Layout;

public abstract record GridTemplateTrack(Number Size)
{
    public override string ToString() => $"[{Size}]";
}

public record GridRowTemplate(Number Size) : GridTemplateTrack(Size);
public record GridColTemplate(Number Size) : GridTemplateTrack(Size);
