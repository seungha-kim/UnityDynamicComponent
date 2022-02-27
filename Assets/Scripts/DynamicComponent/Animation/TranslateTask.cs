using System;
using UnityEngine;

namespace DynamicComponent
{
    class TranslateTask : AnimationTask
    {
        private Vector3 _translation;
        private TimeSpan _elapsed = TimeSpan.Zero;
        private TimeSpan _duration;
        private EasingFunc _easingFunc;

        public TranslateTask(GameObject target, Vector3 translation, TimeSpan duration, EasingKind easing) : base(
            target, false)
        {
            _translation = translation;
            _duration = duration;
            _easingFunc = EasingFuncs.GetEasingFunc(easing);
        }

        public override void Update()
        {
            var delta = TimeSpan.FromSeconds(Time.deltaTime);
            _elapsed += delta;
            var progress = _elapsed.TotalSeconds / _duration.TotalSeconds;
            var eased = _easingFunc(progress);
            // TODO: stackable true
            Target.transform.position = (float) eased * _translation;
        }

        public override bool IsEnded()
        {
            return _elapsed > _duration;
        }
    }
}