using UnityEngine;

namespace mech.input
{
    public interface IEventHandlerThreeButtonRunner : IEventHandler
    {
        void OnButtonPress(Vector3 vecMove, bool isJump);
    }
}