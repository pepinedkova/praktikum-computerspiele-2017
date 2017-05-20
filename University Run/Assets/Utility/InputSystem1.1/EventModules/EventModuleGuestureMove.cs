using UnityEngine;
using System.Collections.Generic;

namespace mech.input
{
    public class EventModuleGuestureMove : AEventModule
    {
        public EventModuleGuestureMove(IEventHandlerMoveGuesture reciever)
        {
            this.reciever = reciever;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            if(touchDatas.Count == 2)
            foreach(TouchData td1 in touchDatas)
            {
                foreach(TouchData td2 in touchDatas)
                {
                    if(td1 != td2)
                    {
                        (reciever as IEventHandlerMoveGuesture).OnMoveGuesture(td1, td2);
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