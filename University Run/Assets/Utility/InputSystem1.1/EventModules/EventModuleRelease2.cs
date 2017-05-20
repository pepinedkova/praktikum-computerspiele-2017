using UnityEngine;
using System.Collections.Generic;

namespace mech.input
{
    public class EventModuleRelease2 : AEventModule
    {
        List<TouchData> tdsReleased; 
        float timeSinceLastRelease = 0f;
        float timeDistTouchesMax;

        public EventModuleRelease2()
        {
            tdsReleased = new List<TouchData>();
            this.timeDistTouchesMax = 0.4f;
        }

        public EventModuleRelease2(float timeDistTouchesMax)
        {
            tdsReleased = new List<TouchData>();
            this.timeDistTouchesMax = timeDistTouchesMax;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {
            timeSinceLastRelease += Time.deltaTime;

            if(timeSinceLastRelease > timeDistTouchesMax) tdsReleased.Clear();

            if(tdsReleased.Count == 0)
            {
                foreach(TouchData td in touchDatas)
                {
                    if(td.monoOrigin is IEventHandlerRelease2)
                    if(td.phasePointer == PhasePointer.ENDED)
                    {
                        timeSinceLastRelease = 0f;
                        tdsReleased.Add(td);
                        if(tdsReleased.Count > 2)
                            for(int index = 2; index < tdsReleased.Count; index++)
                                tdsReleased.RemoveAt(0);
                        break;
                    }
                }
            }

            if(tdsReleased.Count == 1)
            {
                foreach(TouchData td in touchDatas)
                {
                    if(td != tdsReleased[0])
                    if(td.monoOrigin is IEventHandlerRelease2)
                    if(td.phasePointer == PhasePointer.ENDED)
                    if(timeSinceLastRelease < timeDistTouchesMax)
                    {
                        (td.monoOrigin as IEventHandlerRelease2).OnReleases2(td, tdsReleased[0]);
                        tdsReleased.Clear();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}