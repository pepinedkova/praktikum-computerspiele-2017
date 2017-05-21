using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleSnapPlate : AEventModule
    {
        private Image plate;
        private Vector2 vecDeltaCamera;

        public EventModuleSnapPlate(IEventHandlerSnapPlate reciever, Image plate)
        {
            this.reciever = reciever;
            this.plate = plate;
            vecDeltaCamera = Vector2.zero;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {
            bool foundMove = false;
            foreach (TouchData td in touchDatas)
            {
                if 
                (
                    UtilityInput.IsPointOverAlpha(plate, td.position) &&
                    UtilityInput.IsPointOverAlpha(plate, td.posOrigin) &&
                    (td.phasePointer == PhasePointer.MOVED || td.phasePointer == PhasePointer.STATIC)
                )
                {
                    foundMove = true;
                    if(td.phasePointer == PhasePointer.MOVED)
                    {
                        Vector3 vecPrev = td.posPrev;
                        Vector3 vecNow = td.position;
                        Vector2 vecPrevScreen = Camera.main.WorldToScreenPoint(vecPrev);
                        Vector2 vecNowScreen = Camera.main.WorldToScreenPoint(vecNow);

                        Vector2 vecDelta = vecNow - vecPrev; // vecNowScreen - vecPrevScreen;
                        vecDeltaCamera += vecDelta;
                    }
                }
            }

            if (!foundMove) vecDeltaCamera = Vector2.zero;

            Debug.Log("found " + foundMove + touchDatas.Count);

            if (isNotifier) (reciever as IEventHandlerSnapPlate).OnMoved(vecDeltaCamera);

            return true;
        }
    }
}