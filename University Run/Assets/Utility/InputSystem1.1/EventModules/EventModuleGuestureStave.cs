using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleGuestureStrave : AEventModule
    {
        public EventModuleGuestureStrave(IEventHandlerStrafeGuesture reciever)
        {
            this.reciever = reciever;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            if(touchDatas.Count == 1)
            foreach(TouchData td in touchDatas)
            {
                if(td.phasePointer == PhasePointer.MOVED)
                {
                    (reciever as IEventHandlerStrafeGuesture).OnStraveGuesture(td);
                    return true;
                }
            }

#endif

#if UNITY_EDITOR

#endif

             return false;
        }
    }
}