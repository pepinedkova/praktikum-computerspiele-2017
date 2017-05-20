using System.Collections.Generic;

namespace mech.input
{
    public class EventModuleHold : AEventModule
    {
        private float timeHold;

        public EventModuleHold()
        {
            timeHold = 0.66f;
        }

        public EventModuleHold(float timeHold)
        {
            this.timeHold = timeHold;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if(td.monoCurrent is IEventHandlerHold) // objects wants to know
                {
                    if
                    (
                        td.monoCurrent != null && // is over object
                        !td.hasSentHold && // not sent yet
                        td.timeOnObject >= timeHold // hold time excelled
                    )
                    {
                        if(isNotifier)(td.monoCurrent as IEventHandlerHold).OnHold(td);
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