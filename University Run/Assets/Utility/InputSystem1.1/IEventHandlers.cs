using UnityEngine;

namespace mech.input
{
    public interface IEventHandler{}

    public interface IEventHandlerPress : IEventHandler
    {
        void OnPress(TouchData td);
    }

    public interface IEventHandlerRelease : IEventHandler
    {
        void OnRelease(TouchData td);
    }

    public interface IEventHandlerEnter : IEventHandler
    {
        void OnEnter(TouchData td);
    }

    public interface IEventHandlerExit : IEventHandler
    {
        void OnExit(TouchData td);
    }

    public interface IEventHandlerHold : IEventHandler
    {
        void OnHold(TouchData td);
    }

    public interface IEventHandlerTap : IEventHandler
    {
        void OnTap(TouchData td);
    }

    public interface IEventHandlerDrag : IEventHandler
    {
        void OnDrag(TouchData td);
    }

    public interface IEventHandlerSwipe : IEventHandler
    {
        void OnSwipe(TouchData td);
    }

    public interface IEventHandlerTapSwipe : IEventHandler
    {
        void OnTapSwipe(TouchData td);
    }

    // move 

    public interface IEventHandlerLook: IEventHandler
    {
        void OnLook(TouchData td);
    }

    public interface IEventHandlerVirtualJoystick : IEventHandler
    {
        void OnMoveSixAxis(Vector3 vecMove);
    }
   
    public interface IEventHandlerMoveGuesture: IEventHandler
    {
        void OnMoveGuesture(TouchData td1, TouchData td2);
    }

    public interface IEventHandlerStrafeGuesture: IEventHandler
    {
        void OnStraveGuesture(TouchData td);
    }

    // 2 finger

    public interface IEventHandlerPress2 : IEventHandler
    {
        void OnPress2(TouchData td1, TouchData td2);
    }

    public interface IEventHandlerRelease2 : IEventHandler
    {
        void OnReleases2(TouchData td1, TouchData td2);
    }

    public interface IEventHandlerSwipe2 : IEventHandler
    {
        void OnSwipe2(TouchData td1, TouchData td2);
    }

    // selection

    public interface IEventHandlerSelectSwipe : IEventHandler
    {
        void OnSelectSwipe(TouchData td1, TouchData td2);
    }
}