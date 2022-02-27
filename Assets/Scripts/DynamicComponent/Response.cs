using System;
using UnityEngine;

namespace DynamicComponent
{
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
                _ => new DummyResponse(),
            };
        }
    }
    
    public class MoveToResponse: Response
    {
        public Vector3 Destination;
        public EasingKind Easing;
        public float DurationInSeconds;
    }

    public class DummyResponse: Response
    {
        
    }
}