using UnityEngine;
using System.Collections.Generic;

namespace mech.input
{
    public class EventModulePress : AEventModule
    {
        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if
                (
                    td.monoOrigin is IEventHandlerPress &&
                    td.phasePointer == PhasePointer.BEGAN
                )
                {
                    if(isNotifier) (td.monoOrigin as IEventHandlerPress).OnPress(td);
                    return true;
                }
            }

#endif

#if UNITY_EDITOR

            //foreach(TouchData td in touchDatas)
            //{
            //    if
            //    (
            //        td.monoOrigin is IEventHandlerPress &&
            //        td.phase == TouchPhase.Began
            //    )
            //    {
            //        if(isNotifier) (td.monoOrigin as IEventHandlerPress).OnPress(td);
            //        return true;
            //    }
            //} 

#endif

            return false;
        }
    }
}