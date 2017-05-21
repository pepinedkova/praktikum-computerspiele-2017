using System;
using UnityEngine;
using UnityEngine.UI;

namespace mech.input
{
    public class InterfaceModuleVirtualJoystick : AInterfaceModule
    {
        private Image[] images; // images[ front, back, right, left, up, down ]

        public InterfaceModuleVirtualJoystick(IEventHandlerVirtualJoystick eventHandlerVirtualJoystick, Image[] images)
        {
            this.images = images;
            eventModules.Add(new EventModuleVirtualJoystick(eventHandlerVirtualJoystick, images));
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            foreach(Image image in images)
                if(UtilityInput.IsPointOverAlpha(image, td.position)) return true;
            return false;
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            return !isRelevant;
        }
    }
}