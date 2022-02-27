using System;

namespace DynamicComponent
{
    [Serializable]
    public class Interaction
    {
        public TriggerKind trigger;
        public ResponseKind response;
        public ResponseData ResponseDataData;
    }
}
