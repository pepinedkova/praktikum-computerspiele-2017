using UnityEngine.UI;

namespace mech.input
{
    public class InterfaceModuleThreeStick : AInterfaceModule
    {
        private Image[] buttons;
        // buttons[left, right, up]
        public InterfaceModuleThreeStick(IEventHandlerThreeStick reciever, Image[] buttons)
        {
            this.buttons = buttons;
            eventModules.Add(new EventModuleThreeStick(reciever, buttons));
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            return UtilityInput.IsPointOverAlphas(buttons, td.position);
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            return !isRelevant;
        }
    }
}