using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleExit : AEventModule
    {
        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

    #if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if(td.monoCurrent is IEventHandlerExit)
                {
                    if
                    (
                        td.phasePointer == PhasePointer.ENDED ||
                        (
                            td.phasePointer == PhasePointer.MOVED &&
                            td.monoPrev != null &&
                            td.monoCurrent != td.monoPrev
                        )
                    )
                    {
                        if(isNotifier) (td.monoCurrent as IEventHandlerExit).OnExit(td);
                        return true;
                    }
                }
            }

    #endif

    #if UNITY_EDITOR

    #endif

            return false;  
        }
    }
}