﻿namespace No8.Areaz.Painting.Animation;

public static class AnimationExtensions
{
    public static Task<bool> Animate(this IAnimatable animatable, AnimationDefinition animationDefinition)
    {
		//Control control = (Control)animatable;

		var tcs = new TaskCompletionSource<bool>();
        var weakReference = new WeakReference<IAnimatable>(animatable);
        var animation = animationDefinition.CreateAnimation(animatable);

        // Prevent any opacity challenges when element starts hidden
        if (animationDefinition.OpacityFromZero) animatable.Opacity = 0;

        if ((animationDefinition.PauseBeforeMS > 0) ||
            (animationDefinition.PauseAfterMS > 0) ||
            (animationDefinition.RepeatCount > 1) ||
            (animationDefinition.DelayMS > 0))
        {
            Task.Run(async () =>
            {
                if (animationDefinition.PauseBeforeMS > 0)
                    await Task.Delay(animationDefinition.PauseBeforeMS);
                animation.Commit(animatable, "", 16, animationDefinition.DurationMS, null,
                    async (f, a) => {
                        if (animationDefinition.PauseAfterMS > 0)
                            await Task.Delay(animationDefinition.PauseAfterMS);
                        tcs.SetResult(a);
                    }, null);
            });
        }
        else
            animation.Commit(animatable, "", 16, animationDefinition.DurationMS, null, (f, a) => tcs.SetResult(a), null);

        return tcs.Task;
    }

    public static void ClearTransforms(this IAnimatable animatable)
    {
        animatable.AbortAnimation("");

        animatable.Opacity = 1;
        animatable.TranslationX = 0;
        animatable.TranslationY = 0;
        animatable.Scale = 1;
    }

    private static readonly Dictionary<AnimatableKey, Info> Animations;
    private static readonly Dictionary<AnimatableKey, int> Kinetics;

	static AnimationExtensions()
	{
		Animations = new();
		Kinetics = new();
	}

	public static bool AbortAnimation(this IAnimatable self, string handle)
	{
		var key = new AnimatableKey(self, handle);

		if (!Animations.ContainsKey(key) && 
			!Kinetics.ContainsKey(key))
		{
			return false;
		}

		void Abort()
		{
			AbortAnimation(key);
			AbortKinetic(key);
		}

		DoAction(self, Abort);

		return true;
	}

	public static void Animate(
		this IAnimatable self, 
		string name, 
		Animation animation, 
		uint rate = 16, 
		uint length = 250, 
		Easing? easing = null, 
		Action<float, bool>? finished = null,							   
		Func<bool>? repeat = null)
	{
		if (repeat == null)
			self.Animate(name, animation.GetCallback(), rate, length, easing, finished);
		else
		{
			bool RepeatFunc()
			{
				var val = repeat();
				if (val) animation.ResetChildren();
				return val;
			}

			self.Animate(name, animation.GetCallback(), rate, length, easing, finished, RepeatFunc);
		}
	}

	public static void Animate(
		this IAnimatable self, 
		string name, 
		Action<float> callback,
		float start, 
		float end, 
		uint rate = 16, 
		uint length = 250, 
		Easing? easing = null,
		Action<float, bool>? finished = null, 
		Func<bool>? repeat = null)
	{
		self.Animate(name, Interpolate(start, end), callback, rate, length, easing, finished, repeat);
	}

	public static void Animate(
		this IAnimatable self, 
		string name, 
		Action<float> callback, 
		uint rate = 16, 
		uint length = 250, 
		Easing? easing = null, 
		Action<float, bool>? finished = null,
		Func<bool>? repeat = null)
	{
		self.Animate(name, x => x, callback, rate, length, easing, finished, repeat);
	}

	public static void Animate<T>(
		this IAnimatable self, 
		string name, 
		Func<float, T> transform, 
		Action<T> callback,
		uint rate = 16, 
		uint length = 250, 
		Easing? easing = null,
		Action<T, bool>? finished = null, 
		Func<bool>? repeat = null)
	{
		if (transform == null)
			throw new ArgumentNullException(nameof(transform));
		if (callback == null)
			throw new ArgumentNullException(nameof(callback));
		if (self == null)
			throw new ArgumentNullException(nameof(self));

		void ReAnimate() => AnimateInternal(self, name, transform, callback, rate, length, easing, finished, repeat);
		DoAction(self, ReAnimate);
	}


	public static void AnimateKinetic(
		this IAnimatable self, 
		string name, 
		Func<float, float, bool> callback, 
		float velocity, 
		float drag, 
		Action? finished = null)
	{
		void ReAnimate() => AnimateKineticInternal(self, name, callback, velocity, drag, finished);
		DoAction(self, ReAnimate);
	}

	public static bool AnimationIsRunning(this IAnimatable self, string handle)
	{
		var key = new AnimatableKey(self, handle);
		return Animations.ContainsKey(key);
	}

	public static Func<float, float> Interpolate(
		float start, 
		float end = 1.0f, 
		float reverseVal = 0.0f, 
		bool reverse = false)
	{
		float target = reverse ? reverseVal : end;
		return x => start + (target - start) * x;
	}

	static void AbortAnimation(AnimatableKey key)
	{
		// If multiple animations on the same view with the same name (IOW, the same AnimatableKey) are invoked
		// asynchronously (e.g., from the `[Animate]To` methods in `ViewExtensions`), it's possible to get into 
		// a situation where after invoking the `Finished` handler below `s_animations` will have a new `Info`
		// object in it with the same AnimatableKey. We need to continue cancelling animations until that is no
		// longer the case; thus, the `while` loop.

		// If we don't cancel all of the animations popping in with this key, `AnimateInternal` will overwrite one
		// of them with the new `Info` object, and the overwritten animation will never complete; any `await` for
		// it will never return.

		while (Animations.ContainsKey(key))
		{
			Info info = Animations[key];

			Animations.Remove(key);

			info.Tweener.ValueUpdated -= HandleTweenerUpdated;
			info.Tweener.Finished -= HandleTweenerFinished;
			info.Tweener.Stop();
			info.Finished?.Invoke(1.0f, true);
		}
	}

	static void AbortKinetic(AnimatableKey key)
	{
		if (!Kinetics.ContainsKey(key))
		{
			return;
		}

		Ticker.Default.Remove(Kinetics[key]);
		Kinetics.Remove(key);
	}

	static void AnimateInternal<T>(
		IAnimatable self, 
		string name, 
		Func<float, T> transform, 
		Action<T> callback,
		uint rate, 
		uint length, 
		Easing? easing, 
		Action<T, bool>? finished, 
		Func<bool>? repeat)
	{
		var key = new AnimatableKey(self, name);

		AbortAnimation(key);

		void Step(float f) => callback(transform(f));
		Action<float, bool>? final = null;
		if (finished != null)
			final = (f, b) => finished(transform(f), b);

		var info = new Info(
			key,
			rate, length, 
			easing ?? Easings.Linear, 
			Step, final, repeat);

		Animations[key] = info;
		info.Callback(0.0f);
		info.Tweener.Start();
	}

	static void AnimateKineticInternal(
		IAnimatable self, 
		string name, 
		Func<float, float, bool> callback, 
		float velocity, 
		float drag, 
		Action? finished = null)
	{
		var key = new AnimatableKey(self, name);

		AbortKinetic(key);

		float sign = velocity / Math.Abs(velocity);
		velocity = Math.Abs(velocity);

		int tick = Ticker.Default.Insert(step =>
		{
			long ms = step;

			velocity -= drag * ms;
			velocity = Math.Max(0, velocity);

			var result = false;
			if (velocity > 0)
			{
				result = callback(sign * velocity * ms, velocity);
			}

			if (!result)
			{
				finished?.Invoke();
				Kinetics.Remove(key);
			}
			return result;
		});

		Kinetics[key] = tick;
	}

	private static void HandleTweenerFinished(object? obj, EventArgs args)
	{
		if (obj is Tweener tweener && Animations.TryGetValue(tweener.Handle!, out var info))
		{
			info.Callback(tweener.Value);

			var repeat = false;

			if (info.Repeat != null)
				repeat = info.Repeat();

			if (!repeat)
			{
				Animations.Remove(tweener.Handle!);
				tweener.ValueUpdated -= HandleTweenerUpdated;
				tweener.Finished -= HandleTweenerFinished;
			}

			info.Finished?.Invoke(tweener.Value, false);

			if (repeat)
			{
				tweener.Start();
			}
		}
	}

	static void HandleTweenerUpdated(object? obj, EventArgs args)
	{
		if (obj is Tweener tweener && 
			Animations.TryGetValue(tweener.Handle!, out var info) && 
			info.Owner.TryGetTarget(out _))
		{
			info.Callback(info.Easing.Ease(tweener.Value));
		}
	}

	static void DoAction(IAnimatable self, Action action)
	{
		// TODO: Execute on main thread?
		action();
	}

	class Info
	{
		public Action<float> Callback { get; }
		public Action<float, bool>? Finished { get; }
		public Func<bool>? Repeat { get; }
		public Tweener Tweener { get; }

		public Easing Easing { get; set; }

		public uint Length { get; set; }

		public WeakReference<IAnimatable> Owner { get; set; }

		public uint Rate { get; set; }


		public Info(
			AnimatableKey key,
			uint rate, 
			uint length, 
			Easing easing,
			Action<float> callback,
			Action<float, bool>? finished,
			Func<bool>? repeat)
		{
			Owner = key.Animatable;
			Rate = rate;
			Length = length;
			Easing = easing;
			Callback = callback;
			Finished = finished;
			Repeat = repeat;

			Tweener = new Tweener(length, rate)
			{
				Handle = key
			};
			Tweener.ValueUpdated += HandleTweenerUpdated;
			Tweener.Finished += HandleTweenerFinished;
		}
	}
}