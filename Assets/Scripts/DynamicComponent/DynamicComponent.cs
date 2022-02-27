using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DynamicComponent
{
    public class DynamicComponent : MonoBehaviour
    {
        private AnimationTaskScheduler _scheduler;

        private readonly List<Interaction> _interactions = new List<Interaction>()
        {
            new Interaction()
            {
                response = new MoveToResponse()
                {
                    destination = new Vector3(5, 0, 0),
                    durationInSeconds = 5,
                    easing = EasingKind.Linear,
                },
                trigger = TriggerKind.Click,
            },
            // TODO: stackable
            // new Interaction()
            // {
            //     response = new MoveToResponse()
            //     {
            //         destination = new Vector3(0, 5, 0),
            //         durationInSeconds = 3,
            //         easing = EasingKind.Linear,
            //     },
            //     trigger = TriggerKind.Click,
            // }
        };

        private void Start()
        {
            _scheduler = new AnimationTaskScheduler();
        }

        public void Update()
        {
            _scheduler.Update();

            if (IsClicked())
            {
                foreach (var i in _interactions)
                {
                    if (i.trigger is TriggerKind.Click)
                    {
                        _scheduler.AddTask(AnimationTask.FromResponse(i.response, gameObject));
                    }
                }
            }
        }

        private bool IsClicked()
        {
            if (!Input.GetMouseButtonDown(0)) return false;
            var cam = Camera.main!;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out _);
        }

        [CustomEditor(typeof(DynamicComponent))]
        public class DynamicComponentEditor : Editor
        {
            void OnEnable()
            {
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                {
                    EditorGUILayout.LabelField("Dynamic component gui");
                    var gameObject = (DynamicComponent) target;
                    foreach (var i in gameObject._interactions)
                    {
                        EditorGUILayout.LabelField(
                            $"{Enum.GetName(typeof(TriggerKind), i.trigger)}, {i.response.GetType().Name}");
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}