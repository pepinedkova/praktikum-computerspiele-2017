using UnityEngine;

namespace mech.input
{
    public interface IEventHandlerSnapPlate : IEventHandler
    {
        void OnMoved(Vector2 vecDistToFocus);
    }
}