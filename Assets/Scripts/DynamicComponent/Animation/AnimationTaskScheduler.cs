using System;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicComponent
{
    class AnimationTaskScheduler
    {
        private readonly List<AnimationTask> _tasks = new List<AnimationTask>();

        public void AddTask(AnimationTask task)
        {
            _tasks.Add(task);
        }

        public void Update()
        {
            var ended = new List<int>();
            for (int i = 0; i < _tasks.Count; i++)
            {
                var t = _tasks[i];
                t.Update();
                if (t.IsEnded())
                {
                    ended.Add(i);
                }
            }

            for (var i = ended.Count - 1; i >= 0; i--)
            {
                _tasks.RemoveAt(i);
            }
        }
    }
}