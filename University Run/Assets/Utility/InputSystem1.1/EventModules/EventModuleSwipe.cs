using System.Collections.Generic;

namespace mech.input
{
    public class EventModuleSwipe : AEventModule
    {
        private float lengthRequired;

        public EventModuleSwipe()
        {
            lengthRequired = 25f;
        }

        public EventModuleSwipe(float lengthRequired)
        {
            this.lengthRequired = lengthRequired;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if(td.monoOrigin is IEventHandlerSwipe)
                if(td.distSwipe >= lengthRequired)
                {
                    td.nSwipes++;
                    td.dirSwipe = Vec2Dir(td.position - td.posOrigin);

                    (td.monoOrigin as IEventHandlerSwipe).OnSwipe(td);

                    td.distSwipe = 0f;
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