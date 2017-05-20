using System.Collections.Generic;

namespace mech.input
{
    public class EventModulePress2 : AEventModule
    {
        private float TIME_MAX_DISTANCE_TOUCHES;

        private List<TouchData> tdsLocked;

        public EventModulePress2()
        {
            tdsLocked = new List<TouchData>();
            TIME_MAX_DISTANCE_TOUCHES = 0.33f;
        }

        public EventModulePress2(float timeDistTouchesMax)
        {
            tdsLocked = new List<TouchData>();
            TIME_MAX_DISTANCE_TOUCHES = timeDistTouchesMax;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {
            foreach(TouchData td1 in touchDatas)
            {
                foreach(TouchData td2 in touchDatas)
                {
                    if
                    (
                        !IsTouchLocked(td1) && !IsTouchLocked(td2) && // free for this guesture
                        td1 != td2 && // not same touches
                        td1.monoCurrent == td2.monoCurrent &&  // same object
                        td1.timeOnObject <= TIME_MAX_DISTANCE_TOUCHES && td2.timeOnObject <= TIME_MAX_DISTANCE_TOUCHES // touches are both new
                    )
                    {
                        if(td1.monoCurrent is IEventHandlerPress2)
                        {
                            (td1.monoCurrent as IEventHandlerPress2).OnPress2(td1, td2);
                            LockTouches(td1, td2);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void LockTouches(TouchData td1, TouchData td2)
        {
            tdsLocked.Add(td1);
            tdsLocked.Add(td2);
            if(tdsLocked.Count > 10)
            {
                tdsLocked.RemoveAt(0);
                tdsLocked.RemoveAt(0);
            }
        }

        private bool IsTouchLocked(TouchData td)
        {
            foreach(TouchData tdLocked in tdsLocked)
            {
                if(td == tdLocked) return true;
            }
            return false;
        }
    }
}