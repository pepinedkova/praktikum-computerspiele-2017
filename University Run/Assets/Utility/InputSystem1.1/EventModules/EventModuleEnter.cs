using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleEnter : AEventModule
    {
        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

    #if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if(td.monoCurrent != null)
                if(td.monoCurrent is IEventHandlerEnter)
                {
                    if
                    (
                        td.phasePointer == PhasePointer.BEGAN ||       
                        (
                            td.phasePointer == PhasePointer.MOVED &&
                            td.monoPrev != td.monoCurrent
                        )
                    )
                    {
                        if(isNotifier) (td.monoCurrent as IEventHandlerEnter).OnEnter(td);
                        return true;
                    }
                }
            }

    #endif

             return false;
        }
    }
}