using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleVirtualJoystick : AEventModule
    {
        private Image[] images;

        public EventModuleVirtualJoystick(IEventHandlerVirtualJoystick reciever, Image[] images)
        {
            this.reciever = reciever;
            this.images = images;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            Vector3 vecMove = Vector3.zero;

            foreach(TouchData td in touchDatas)
            {               
                if(UtilityInput.IsPointOverAlpha(images[0], td.position)) vecMove.z++;
                if(UtilityInput.IsPointOverAlpha(images[1], td.position)) vecMove.z--;
                if(UtilityInput.IsPointOverAlpha(images[2], td.position)) vecMove.x++;
                if(UtilityInput.IsPointOverAlpha(images[3], td.position)) vecMove.x--;
                if(UtilityInput.IsPointOverAlpha(images[4], td.position)) vecMove.y++;
                if(UtilityInput.IsPointOverAlpha(images[5], td.position)) vecMove.y--;
            }

            if(vecMove != Vector3.zero)
            {
                vecMove.Normalize();
                (reciever as IEventHandlerVirtualJoystick).OnMoveSixAxis(vecMove);
                return true;
            }

#endif

#if UNITY_EDITOR

#endif

            return false;
        }
    }
}