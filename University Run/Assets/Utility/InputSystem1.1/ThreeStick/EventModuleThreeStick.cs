using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

namespace mech.input
{
    public class EventModuleThreeStick : AEventModule
    {
        private Image[] buttons;

        public EventModuleThreeStick(IEventHandlerThreeStick reciever, Image[] buttons)
        {
            this.reciever = reciever;
            this.buttons = buttons;
        }

        public override bool SearchEvent(List<TouchData> touchDatas, float tDelta)
        {
            Vector3 vecMove = Vector3.zero;
            bool isJumping = false;

            foreach (TouchData td in touchDatas)
            {
                if (UtilityInput.IsPointOverAlpha(buttons[0], td.position)) vecMove.x--;
                if (UtilityInput.IsPointOverAlpha(buttons[1], td.position)) vecMove.x++;
                if (UtilityInput.IsPointOverAlpha(buttons[2], td.position)) isJumping = true;
            }

            if (vecMove != Vector3.zero || isJumping)
            {
                vecMove.Normalize();
                (reciever as IEventHandlerThreeStick).OnButtonPress(vecMove, isJumping);
                return true;
            }

            return false;
        }
    }
}