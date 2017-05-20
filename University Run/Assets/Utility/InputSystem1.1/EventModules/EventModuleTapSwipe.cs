using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleTapSwipe : AEventModule
    {
        private float lengthRequired;
        private float timeInterval;

        private float timeTapStart = 0.5f;
        private float timeTap = 0.5f;

        public EventModuleTapSwipe()
        {
            lengthRequired = 25f;
            timeInterval = 0.12f;
        }

        public EventModuleTapSwipe(float lengthRequired, float timeInterval)
        {
            this.lengthRequired = lengthRequired;
            this.timeInterval = timeInterval;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            if(touchDatas.Count != 1) return false;

            foreach(TouchData td in touchDatas)
            {
                if(td.nSwipes == 0)
                if(td.phasePointer == PhasePointer.ENDED)
                if(td.timeTouch < timeInterval)
                if(td.monoCurrent == td.monoOrigin)
                if(td.monoCurrent is IEventHandlerTapSwipe)
                {
                    timeTap = timeTapStart;
                }
            }

            if(timeTap > 0) timeTap -= Time.deltaTime;
            if(timeTap < 0) timeTap = 0;

            if(timeTap > 0)
            foreach(TouchData td in touchDatas)
            {
                if(td.monoOrigin is IEventHandlerTapSwipe)
                if(td.distSwipe >= lengthRequired)
                {
                    td.nSwipes++;
                    td.dirSwipe = Vec2Dir(td.position - td.posOrigin);

                    (td.monoOrigin as IEventHandlerTapSwipe).OnTapSwipe(td);

                    td.distSwipe = 0f;

                    timeTap = 0;
                    return true;
                }
            }

#endif

#if UNITY_EDITOR

#endif

            return false;
        }
    }
}