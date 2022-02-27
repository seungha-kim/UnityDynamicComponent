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

    public abstract class ResponseData
    {
        public static ResponseData FromKind(ResponseKind kind)
        {
            return kind switch
            {
                ResponseKind.MoveTo => new MoveToResponseData(),
            };
        }
    }
    
    [Serializable]
    public class MoveToResponseData: ResponseData
    {
        public Vector3 destination;
        public EasingKind easing;
        public float durationInSeconds;
    }
}