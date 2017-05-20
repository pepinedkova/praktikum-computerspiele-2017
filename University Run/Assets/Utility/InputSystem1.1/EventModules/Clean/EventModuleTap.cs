using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleTap : AEventModule
    {
        public float timePressMax = 0.33f;

        public EventModuleTap(){}

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if
                (
                    td.phasePointer == PhasePointer.ENDED &&
                    td.timeTouch < timePressMax &&
                    td.monoCurrent == td.monoOrigin
                )
                {
                    if(isNotifier)(td.monoOrigin as IEventHandlerTap).OnTap(td);
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