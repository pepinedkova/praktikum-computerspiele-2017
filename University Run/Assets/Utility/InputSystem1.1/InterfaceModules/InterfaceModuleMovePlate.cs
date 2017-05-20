using UnityEngine.UI;
using UnityEngine;
using System;

namespace mech.input
{
    public class InterfaceModuleMovePlate : AInterfaceModule
    {
        private Image maskOverlay;

        public InterfaceModuleMovePlate(Image maskOverlay, IEventHandlerMoveGuesture eventHandlerGuestureMove, IEventHandlerStrafeGuesture eventHandlerGuestureStrave)
        {
            this.maskOverlay = maskOverlay;
            eventModules.Add(new EventModuleGuestureMove(eventHandlerGuestureMove));
            eventModules.Add(new EventModuleGuestureStrave(eventHandlerGuestureStrave));
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            return UtilityInput.IsPointOverAlpha(maskOverlay, td.position);
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            return !isRelevant;
        }
    }
}