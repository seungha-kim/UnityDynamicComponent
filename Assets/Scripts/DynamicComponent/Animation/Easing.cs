namespace DynamicComponent
{
    internal delegate double EasingFunc(double progress);

    static class EasingFuncs
    {
        public static EasingFunc Linear = (progress) => progress;

        public static EasingFunc GetEasingFunc(EasingKind easing)
        {
            return easing switch
            {
                EasingKind.Linear => Linear,
                _ => Linear
            };
        }
    }
}