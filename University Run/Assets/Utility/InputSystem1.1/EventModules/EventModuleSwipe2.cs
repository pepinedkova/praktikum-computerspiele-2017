using UnityEngine;
using System.Collections.Generic;

namespace mech.input
{
    public class EventModuleSwipe2 : AEventModule
    {
        float lengthRequired;
        float lengthDistTouchesMax;

        public EventModuleSwipe2()
        {
            lengthRequired = 16f;
            lengthDistTouchesMax = 8f;
        }

        public EventModuleSwipe2(float lengthRequired, float lengthDistTouchesMax)
        {
            this.lengthRequired = lengthRequired;
            this.lengthDistTouchesMax = lengthDistTouchesMax;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            if(touchDatas.Count == 2)
            if(GetTouchesOnOrigin(touchDatas) == 2)
            foreach(TouchData td1 in touchDatas)
            {
                foreach(TouchData td2 in touchDatas)
                {
                    //if(td1.isFreeForEvent && td2.isFreeForEvent)
                    if(td1 != td2)
                    if(td1.monoOrigin is IEventHandlerSwipe2 && td2.monoOrigin is IEventHandlerSwipe2)
                    if(td1.distSwipe >= lengthRequired && td2.distSwipe >= lengthRequired)
                    if(Mathf.Abs(td1.distSwipe - td2.distSwipe) <= lengthDistTouchesMax)
                    if(Vector2.Angle(td1.deltaSwipe, td2.deltaSwipe) < 35)
                    {
                        int dirSwipe0 = GetDirSwipe(td1.deltaSwipe);
                        int dirSwipe1 = GetDirSwipe(td1.deltaSwipe);

                        if(dirSwipe0 == dirSwipe1)
                        {
                            // td1.isFreeForEvent = false;
                            // td2.isFreeForEvent = false;

                            td1.nSwipes++;
                            td2.nSwipes++;

                            td1.dirSwipe = dirSwipe0;
                            td2.dirSwipe = dirSwipe1;

                            if(isNotifier) (td1.monoOrigin as IEventHandlerSwipe2).OnSwipe2(td1, td2);

                            td1.distSwipe = 0f;
                            td2.distSwipe = 0f;

                            td1.deltaSwipe = Vector2.zero;
                            td2.deltaSwipe = Vector2.zero;

                            return true;
                        }
                    }
                }
            }

#endif

#if UNITY_EDITOR

#endif

             return false;
        }
   
        //int GetTouchesOnOrigin(List<TouchData> touchDatas)
        //{
        //    int nTouches = 0;
        //    foreach(TouchData td in touchDatas)
        //    {
        //        if(td.monoOrigin != null) nTouches++;
        //    }
        //    return nTouches;
        //}

        int GetDirSwipe(Vector2 delta)
        {
            if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                return (delta.x > 0) ? 0 : 1; // right, left
            }
            else
            {
                return (delta.y > 0) ? 2 : 3; // up, down
            }
        }
    }
}