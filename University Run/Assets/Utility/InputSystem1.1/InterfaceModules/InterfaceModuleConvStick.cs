using System;
using UnityEngine;
using UnityEngine.UI;

namespace mech.input
{
    public class InterfaceModuleConvStick : AInterfaceModule
    {
        private Image imgBack;
        private Image imgFront;

        public InterfaceModuleConvStick(IEventHandlerVirtualJoystick reciever, Image imgBack, Image imgFront)
        {
            this.imgBack = imgBack;            
            this.imgFront = imgFront;

            //typeRelevant = TypeRelevant.AT_ORIGIN;
            //typeSpace = TypeSpace.SCREEN;
            //typeFallThrough = TypeFallthrough.NOT_RELEVANT;

            eventModules.Add(new EventModuleConvStick(reciever, imgBack, imgFront));
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            // better radius max for quit
            // started over image
            return UtilityInput.IsPointOverAlpha(imgBack, td.posOrigin);
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            return !isRelevant;
        }
    }
}