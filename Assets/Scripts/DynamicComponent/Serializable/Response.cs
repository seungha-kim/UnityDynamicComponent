using System;
using UnityEngine;

namespace DynamicComponent
{
    [Serializable]
    public enum ResponseKind
    {
        MoveTo,
        MoveBy,
        RotateTo,
        RotateBy,
        ScaleTo,
        ScaleBy
    }

    public abstract class Response
    {
        public static Response FromKind(ResponseKind kind)
        {
            return kind switch
            {
                ResponseKind.MoveTo => new MoveToResponse(),
            };
        }
    }
    
    [Serializable]
    public class MoveToResponse: Response
    {
        public Vector3 destination;
        public EasingKind easing;
        public float durationInSeconds;
    }
}