using System.Drawing;

namespace No8.Areaz.Painting.Animation;

public class ScaleInAnimation : AnimationDefinition
{
    public float StartScale = 0.7f;

    public ScaleInAnimation()
    {
        DurationMS = 8;
        OpacityFromZero = true;
    }

    public override Animation CreateAnimation(IAnimatable element)
    {
        var animation = new Animation();

        animation.WithConcurrent((f) => element.Opacity = f, 0, 1, null, 0, 0.5f);
        animation.WithConcurrent((f) => element.Scale = f, StartScale, 1, Easings.CubicOut);

        return animation;
    }

}

public class ScaleOutAnimation : AnimationDefinition
{
    public float EndScale = 0.7f;

    public ScaleOutAnimation()
    {
        DurationMS = 400;
    }

    public override Animation CreateAnimation(IAnimatable element)
    {
        Animation animation = new();

        animation.WithConcurrent((f) => element.Opacity = f, 1, 0, null, 0.5f, 1);
        animation.WithConcurrent((f) => element.Scale = f, element.Scale, EndScale, Easings.CubicIn);

        return animation;
    }
}

public class ScaleFromElementAnimation<TControl> : AnimationDefinition where TControl : IAnimatable
{
    public TControl? FromElement { get; init; }

    public ScaleFromElementAnimation()
    {
        OpacityFromZero = true;
        DurationMS = 400;
    }

    public override Animation CreateAnimation(IAnimatable element)
    {
        TControl control = (TControl)element;
        var toBounds = element.Bounds;
        var fromBounds = FromElement!.Bounds;

        control.SetLayoutBounds(fromBounds);
        element.ClearTransforms();

        Animation animation = new();

        animation.WithConcurrent((f) => element.Opacity = f, 0, 1, null, 0, 0.25f);
        animation.WithConcurrent((f) =>
        {
            var newBounds = new RectangleF(
                            fromBounds.X + (toBounds.X - fromBounds.X) * f,
                            fromBounds.Y + (toBounds.Y - fromBounds.Y) * f,
                            fromBounds.Width + (toBounds.Width - fromBounds.Width) * f,
                            fromBounds.Height + (toBounds.Height - fromBounds.Height) * f);
            control.SetLayoutBounds(newBounds);
        });

        return animation;
    }
}