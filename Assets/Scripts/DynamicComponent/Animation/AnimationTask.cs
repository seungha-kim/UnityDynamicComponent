using System;
using UnityEngine;

namespace DynamicComponent
{
    internal abstract class AnimationTask
    {
        protected GameObject Target { get; }
        protected bool Stackable { get; }

        protected AnimationTask(GameObject target, bool stackable)
        {
            this.Target = target;
            this.Stackable = stackable;
        }

        public abstract void Update();

        public abstract bool IsEnded();

        public static AnimationTask FromResponse(Response response, GameObject obj)
        {
            return response switch
            {
                MoveToResponse r => new TranslateTask(obj, r.destination - obj.transform.position,
                    TimeSpan.FromSeconds(r.durationInSeconds), r.easing),
            };
        }
    }
}