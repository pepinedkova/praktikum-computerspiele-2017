using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleRelease : AEventModule
    {
        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if(td.monoOrigin is IEventHandlerRelease)
                {
                    if(td.phasePointer == PhasePointer.ENDED)
                    {
                        if(isNotifier)(td.monoOrigin as IEventHandlerRelease).OnRelease(td);
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