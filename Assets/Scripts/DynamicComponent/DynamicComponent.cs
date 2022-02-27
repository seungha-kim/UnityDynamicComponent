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
                ResponseDataData = new MoveToResponseData()
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
                        _scheduler.AddTask(AnimationTask.FromResponse(i.ResponseDataData, gameObject));
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
                    var gameObject = (DynamicComponent) target;
                    var toBeDeleted = new List<int>();
                    
                    if (GUILayout.Button("New interaction"))
                    {
                        var i = new Interaction()
                        {
                            ResponseDataData = new MoveToResponseData(),
                            trigger = TriggerKind.Click,
                        };
                        gameObject._interactions.Add(i);
                    }
                    
                    EditorGUILayout.LabelField("Dynamic component gui");
                    
                    for (int i = 0; i < gameObject._interactions.Count; i++)
                    {
                        var inter = gameObject._interactions[i];
                        EditorGUILayout.BeginFoldoutHeaderGroup(true, $"Interaction {i}");
                        inter.trigger = (TriggerKind)EditorGUILayout.EnumPopup("Trigger", inter.trigger);
                        inter.response = (ResponseKind)EditorGUILayout.EnumPopup("Response", inter.response);
                        EditorGUILayout.EndFoldoutHeaderGroup();
                        switch (inter.ResponseDataData)
                        {
                            case MoveToResponseData r:
                                r.destination = EditorGUILayout.Vector3Field("Destination", r.destination);
                                r.easing = (EasingKind)EditorGUILayout.EnumPopup("Easing", r.easing);
                                r.durationInSeconds = EditorGUILayout.FloatField("Duration (sec)", r.durationInSeconds);
                                break;
                        }

                        if (GUILayout.Button($"Delete Interaction {i}"))
                        {
                            toBeDeleted.Add(i);
                        }
                    }

                    toBeDeleted.Reverse();
                    foreach (var i in toBeDeleted)
                    {
                        gameObject._interactions.RemoveAt(i);
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}