using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleLook : AEventModule
    {
        public EventModuleLook(IEventHandlerLook reciever)
        {
            this.reciever = reciever;
        }

        Vector3 posPrev;
        bool isSet = false;

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            foreach(TouchData td in touchDatas)
            {
                if(td.phasePointer == PhasePointer.MOVED)
                {
                    (reciever as IEventHandlerLook).OnLook(td);
                    return true;
                }
            }

#endif

#if UNITY_EDITOR

            if(Input.GetMouseButton(0))
            {
                if(!isSet)
                {
                    isSet = true;
                    posPrev = Input.mousePosition;
                }

                Vector3 delta = Input.mousePosition - posPrev;
                posPrev = Input.mousePosition;

                TouchData tdProxy = new TouchData();
                tdProxy.delta = delta;

                (reciever as IEventHandlerLook).OnLook(tdProxy);
                return true;
            }
            else
            {
                isSet = false;
            }

#endif

             return false;
        }
    }
}