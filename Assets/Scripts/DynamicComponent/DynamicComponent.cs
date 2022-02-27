using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DynamicComponent
{
    public class DynamicComponent : MonoBehaviour
    {
        private AnimationTaskScheduler _scheduler;

        private void Start()
        {
            _scheduler = new AnimationTaskScheduler();
            _scheduler.AddTask(new TranslateTask(this.gameObject, Vector3.right * 10, TimeSpan.FromSeconds(5), EasingKind.Linear));
        }

        public void Update()
        {
            _scheduler.Update();
        }
    }
}