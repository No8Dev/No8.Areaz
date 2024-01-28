using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using No8.Areaz.Helpers;
using No8.Areaz.Painting;
using No8.Areaz.VirtualTerminal;

namespace No8.Areaz.Console;

public class ConsoleDriver
{
    public static ConsoleDriver Create()
    {
        if (UnitTestDetector.IsRunningFromNUnit) 
            return new ConsoleDriverNoIO();
        
        if (OperatingSystem.IsWindows())
            return new ConsoleDriverWindows();

        // Linux, MacOS, others
        return new();
    }

    protected Glyph[]? LastCanvas { get; set; }

    public readonly ConcurrentQueue<KeyboardEvent> KbEvents = new();
    public readonly ConcurrentQueue<PointerEvent> PointerEvents = new();

    public Point Pointer { get; protected set; } = Point.Empty;
    public KeyState[] Mouse { get; } = new KeyState[5];
    public KeyState[] VirtualKeys { get; } = new KeyState[256];

    internal virtual void Shutdown() { }

    public event EventHandler<Size>? TerminalResized;
    public event EventHandler<bool>? WindowFocusChange;
    
    
    /// <summary>
    /// The current number of columns in the terminal.
    /// </summary>
    public int Cols { get; protected set; }

    /// <summary>
    /// The current number of rows in the terminal.
    /// </summary>
    public int Rows { get; protected set; }

    /// <summary>
    /// The current left in the terminal.
    /// </summary>
    public int Left { get; protected set; }

    /// <summary>
    /// The current top in the terminal.
    /// </summary>
    public int Top { get; protected set; }

    public bool       IsFocused            { get; protected set; } = true;
    public int        MousePosX            { get; protected set; }
    public int        MousePosY            { get; protected set; }
    public short      MouseWheel           { get; protected set; }
    public short      MouseHorizontalWheel { get; protected set; }
    
    public void AddKeyboardEvent(KeyboardEvent kbEvent)
    {
        Trace.WriteLine(kbEvent);
        KbEvents.Enqueue(kbEvent);
    }
    
    public void AddPointerEvent(PointerEvent pointerEvent)
    {
        Trace.WriteLine(pointerEvent.ToString());
        PointerEvents.Enqueue(pointerEvent);
    }

    public void InvalidateLayout() { }

    #if LINE_BY_LINE_DETECTION
    protected bool LineChanged(Canvas canvas, Glyph[]? lastCanvas, in int line)
    {
        if (lastCanvas == null) 
            return true;
        if (!lastCanvas.Length.Equals(canvas.Size)) 
            return true;

        for (int x = 0; x < canvas.Width; x++)
        {
            var nextGlyph = canvas[line, x];
            var lastGlyph = lastCanvas[line * canvas.Width + x];

            if (nextGlyph != lastGlyph)
                return true;
        }

        return false;
    }
    #endif
    
    public void WriteConsole(Canvas canvas)
    {
#if LINE_BY_LINE_DETECTION
        // Line by Line
        var sb = new StringBuilder(canvas.Width * canvas.Height);
        Color? lastForeground = null;
        Color? lastBackground = null;
        int linesChanged = 0;
        for (int y = 0; y < canvas.Height; y++)
        {
            if (!LineChanged(canvas, LastCanvas, y))
                continue;

            linesChanged++;
            sb.Append(Terminal.Cursor.Set(y + 1, 1));
            for (int x = 0; x < canvas.Width; x++)
            {
                var tc = canvas.Glyphs[y, x];
                if (tc.Fore != lastForeground && tc.Fore != null)
                {
                    sb.Append(Terminal.Color.Fore(tc.Fore.Value.R, tc.Fore.Value.G, tc.Fore.Value.B));
                    lastForeground = tc.Fore;
                }
                if (tc.Back != lastBackground && tc.Back != null)
                {
                    sb.Append(Terminal.Color.Back(tc.Back.Value.R, tc.Back.Value.G, tc.Back.Value.B));
                    lastBackground = tc.Back;
                }
                sb.Append(tc.Chr);
            }
        }
        Write(sb.ToString());
        Debug.WriteLine($"ConsoleChanged( {linesChanged} )");

        if (LastCanvas == null || LastCanvas.Width != canvas.Width || LastCanvas.Height != canvas.Height)
            LastCanvas = new Array2D<Glyph>(canvas.Glyphs);
        else
            canvas.CloneTo(LastCanvas);
#else
        // Glyph by Glyph
        var sb = new StringBuilder(canvas.Width * canvas.Height);
        System.Drawing.Color? lastForeground = null;
        System.Drawing.Color? lastBackground = null;
        int lastIndex = -2;
        int count = 0;

        if (LastCanvas == null || LastCanvas.Length != (canvas.Width * canvas.Height))
            LastCanvas = new Glyph[canvas.Width * canvas.Height];

        for (int y = 0; y < canvas.Height; y++)
        {
            for (int x = 0; x < canvas.Width; x++)
            {
                var index = y * canvas.Width + x;
                var chr = canvas[y, x];
                var lastChr = LastCanvas[index.Clamp(0, LastCanvas.Length - 1)];

                if (lastChr != chr)
                {
                    count++;

                    if (index != lastIndex + 1)
                        sb.Append(Terminal.Cursor.Set(y + 1, x + 1));
                    lastIndex = index;

                    if (chr.Fore != lastForeground && chr.Fore != null)
                    {
                        sb.Append(Terminal.Color.Fore(chr.Fore.Value));
                        lastForeground = chr.Fore;
                    }
                    if (chr.Back != lastBackground && chr.Back != null)
                    {
                        sb.Append(Terminal.Color.Back(chr.Back.Value.R, chr.Back.Value.G, chr.Back.Value.B));
                        lastBackground = chr.Back;
                    }
                    sb.Append(chr.Chr);

                    LastCanvas[index] = (Glyph)chr.Clone();
                }
            }
        }
        Write(sb.ToString());
        Debug.WriteLine($"ConsoleChanged( {count} )");
#endif
    }

    public virtual void Write(string str)
    {
        System.Console.Write(str);
    }
    
    protected void RaiseWindowFocusChange(bool focused) =>
        WindowFocusChange?.Invoke(this, focused);

    protected void RaiseTerminalResized(System.Drawing.Size size) => 
        TerminalResized?.Invoke(this, size);
}

