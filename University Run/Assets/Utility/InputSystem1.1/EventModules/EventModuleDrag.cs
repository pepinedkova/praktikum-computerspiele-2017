using UnityEngine;
using System.Collections.Generic;

namespace mech.input
{
    public class EventModuleDrag : AEventModule
    {
        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {
            foreach(TouchData td in touchDatas)
            {
                if(td.monoOrigin is IEventHandlerDrag)
                {
                    if(td.phasePointer == PhasePointer.MOVED)
                    {
                        if(isNotifier)(td.monoOrigin as IEventHandlerDrag).OnDrag(td);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}