using UnityEngine;
using System.Collections.Generic;

namespace mech.input
{
    public class EventModuleSelectSwipe : AEventModule
    {
        float lengthRequired;

        public EventModuleSelectSwipe()
        {
            lengthRequired = 25f;
        }

        public EventModuleSelectSwipe(float lengthRequired)
        {
            this.lengthRequired = lengthRequired;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            if(GetTouchesOnOrigin(touchDatas) == 2)
            foreach(TouchData td1 in touchDatas)
            {
                //if(td1.isFreeForEvent)
                if(td1.monoOrigin is IEventHandlerSelectSwipe)
                if(td1.distSwipe >= lengthRequired)
                {
                    foreach(TouchData td2 in touchDatas)
                    {
                        if(td1 != td2)
                        //if(td2.isFreeForEvent)
                        if(td2.monoOrigin is IEventHandlerSelectSwipe)
                        if(td2.monoCurrent == td2.monoOrigin)
                        if(td2.timeTouch >= td1.timeTouch)
                        if(td2.phasePointer == PhasePointer.STATIC)
                        //if(td2.timeStatic >= td1.timeTouch)
                        {
                            td1.nSwipes++;
                            td1.dirSwipe = Vec2Dir(td1.deltaSwipe);

                            if(isNotifier)(td2.monoOrigin as IEventHandlerSelectSwipe).OnSelectSwipe(td2, td1);

                            td1.distSwipe = 0f;
                            td1.deltaSwipe = Vector2.zero;
                        }
                    }
                    return true;
                }
            }

#endif

            return false;
        }
  
        //int GetTouchesOnOrigin(Dictionary<int, TouchData> touchData)
        //{
        //    int nTouches = 0;
        //    foreach(TouchData td in touchDatas)
        //    {
        //        if(td.monoOrigin != null) nTouches++;
        //    }
        //    return nTouches;
        //}

        //int GetDirSwipe(Vector2 delta)
        //{
        //    if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        //    {
        //        return (delta.x > 0) ? 0 : 1; // right, left
        //    }
        //    else
        //    {
        //        return (delta.y > 0) ? 2 : 3; // up, down
        //    }
        //}
    }
}