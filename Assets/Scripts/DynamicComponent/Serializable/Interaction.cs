using System;

namespace DynamicComponent
{
    [Serializable]
    public class Interaction
    {
        public TriggerKind trigger;
        public Response response;
    }
}
