using UnityEngine;

namespace mech.input
{
    public interface IEventHandlerThreeStick : IEventHandler
    {
        void OnButtonPress(Vector3 vecMove, bool isJump);
    }
}