using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleConvStick : AEventModule
    {
        private Image 
            imgBack,
            imgFront;

        private RectTransform 
            rectTransBack, 
            rectTransFront;

        private EventModuleTap eventModuleTap;
 
        private const float 
            TIME_TAP_BACK_MAX = 0.333f,
            RATIO_SPEED_STICK = 0.36f,
            RATIO_CENTER = 0.32f,
            RATIO_TOUCH_ACCEPT = 2.5f;

        private float
            timeSinceTap,
            radiusCenter,
            radiusCenterHalf,
            radiusBack,
            sizeRectBack = 0f,
            sizeRectFront = 0f,
            deviationStickMax = 0f,
            distTouchAcceptMax = 0f;
      
        private bool startedBack = false;
        private TouchData tdLegal = null;

        public EventModuleConvStick(IEventHandlerVirtualJoystick reciever, Image imgBack, Image imgFront)
        {
            this.reciever = reciever;
            this.imgBack = imgBack;            
            this.imgFront = imgFront;

            rectTransBack = imgBack.GetComponent<RectTransform>();
            rectTransFront = imgFront.GetComponent<RectTransform>();

            eventModuleTap = new EventModuleTap();
            eventModuleTap.timePressMax = 0.3f;
            eventModuleTap.isNotifier = false;

            timeSinceTap = eventModuleTap.timePressMax + 1;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {

#if UNITY_ANDROID

            Vector3 vecMove = Vector3.zero;
            Vector2 posTouchLocal = Vector2.zero;

            // needs to be calculated in first frame, cant be constructor
            if(sizeRectBack == 0) sizeRectBack = rectTransBack.rect.width;
            if(sizeRectFront == 0) sizeRectFront = rectTransFront.rect.width;
            if(sizeRectBack != 0 && sizeRectFront != 0)
            {
                deviationStickMax = (sizeRectBack - sizeRectFront) / 2;
                radiusBack = (sizeRectBack / 2);
                deviationStickMax = radiusBack - (sizeRectFront / 2);
                radiusCenter = radiusBack * RATIO_CENTER;
                radiusCenterHalf = radiusCenter / 2;
                distTouchAcceptMax = radiusBack * RATIO_TOUCH_ACCEPT;
            }

            timeSinceTap += tDelta;
            if(eventModuleTap.SearchEvent(touchDatas, tDelta)) timeSinceTap = 0f;

            // assumed that all tds are relevant and legal
            foreach(TouchData td in touchDatas)
            {
                Vector2 posTouch = imgBack.transform.InverseTransformPoint(td.position);
                if(posTouch.magnitude > deviationStickMax)
                {
                    posTouchLocal = (posTouch - rectTransBack.rect.center).normalized * deviationStickMax;
                }
                else
                {
                    posTouchLocal = posTouch;
                }

                Vector2 vecTouch = posTouchLocal - rectTransBack.rect.center;
                float distTouch = vecTouch.magnitude;

                //if(distTouch < distTouchAcceptMax)
                //{
                    int dirTouch = Vec2Dir(vecTouch);

                    if(distTouch < radiusCenter)
                    {
                        if(timeSinceTap < TIME_TAP_BACK_MAX) startedBack = true;

                        // TODO remove backmove because bad paradigm
                        vecMove.z++;
                        //if(startedBack) vecMove.z--;
                        //else vecMove.z++;
                    }
                    else
                    {
                        if(dirTouch == 0) vecMove.x++;
                        if(dirTouch == 1) vecMove.x--;
                        if(dirTouch == 2) vecMove.y++;
                        if(dirTouch == 3) vecMove.y--;
                        startedBack = false;
                    }
               // }  
            }

            Vector2 posStickLocal = imgBack.transform.InverseTransformPoint(imgFront.transform.position);

            bool keptStatic = posTouchLocal.magnitude < radiusCenter;

            Vector2 vecToTarget = ((keptStatic)? Vector2.zero : posTouchLocal) - posStickLocal;
            Vector2 vecToTargetScaled = vecToTarget * RATIO_SPEED_STICK;
            rectTransFront.anchoredPosition += vecToTargetScaled;

            if(vecMove != Vector3.zero)
            {
                vecMove.Normalize();
                (reciever as IEventHandlerVirtualJoystick).OnMoveSixAxis(vecMove);
                return true;
            }
            else
            {
                startedBack = false;
                tdLegal = null;
            }

#endif

#if UNITY_EDITOR

            if(Input.GetMouseButton(0))
            {
                Vector2 posTouch = imgBack.transform.InverseTransformPoint(Input.mousePosition);
                if(posTouch.magnitude < deviationStickMax * 3)
                {                
                    (reciever as IEventHandlerVirtualJoystick).OnMoveSixAxis(new Vector3(0,0,1));
                }
            }
            
#endif

            return false;
        }
    }
}