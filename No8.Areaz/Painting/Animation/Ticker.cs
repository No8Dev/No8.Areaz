using System.ComponentModel;
using System.Diagnostics;

namespace No8.Areaz.Painting.Animation;

[EditorBrowsable(EditorBrowsableState.Never)]
internal sealed class Ticker
{
    private static Ticker? _ticker;

	private Timer? _timer;
	private readonly Stopwatch _stopwatch;
	private readonly List<(int tick, Func<long, bool> func)> _timeouts;

	int _count;
	bool _enabled;

	private Ticker()
	{
		_count = 0;
		_timeouts = new();
		_stopwatch = new();
	}

	public static void SetDefault(Ticker ticker) => Default = ticker;
	public static Ticker Default
	{
        get
        {
            _ticker ??= new();
            return _ticker.GetTickerInstance();
        }
		internal set => _ticker = value;
	}

	Ticker GetTickerInstance()
	{
		return _ticker!;
	}

	public int Insert(Func<long, bool> timeout)
	{
		_count++;
		_timeouts.Add(new (_count, timeout));

		if (!_enabled)
		{
			_enabled = true;
			Enable();
		}

		return _count;
	}

	public void Remove(int handle)
	{
		// TODO: On Main Thread?
		RemoveTimeout(handle);
	}

	void RemoveTimeout(int handle)
	{
		_timeouts.RemoveAll(t => t.Item1 == handle);

		if (_timeouts.Count == 0)
		{
			_enabled = false;
			Disable();
		}
	}

	private void DisableTimer()
    {
		_timer?.Dispose();
	}

	private void EnableTimer()
    {
		_timer = new(
			_ => SendSignals(),
			null,
			TimeSpan.FromMilliseconds(15), TimeSpan.FromMilliseconds(15));
	}

	private void SendSignals(int timeStep = -1)
	{
		long step = timeStep >= 0
			? timeStep
			: _stopwatch.ElapsedMilliseconds;

		SendSignals(step);
	}

	private void SendSignals(long step)
	{
		_stopwatch.Reset();
		_stopwatch.Start();

		foreach ((int tick, Func<long, bool> func) in _timeouts.ToArray())
		{
			bool remove = !func(step);
			if (remove)
				_timeouts.RemoveAll(t => t.tick == tick);
		}

		if (_timeouts.Count == 0)
		{
			_enabled = false;
			Disable();
		}
	}

	void Disable()
	{
		_stopwatch.Reset();
		DisableTimer();
	}

	void Enable()
	{
		_stopwatch.Start();
		EnableTimer();
	}
}
