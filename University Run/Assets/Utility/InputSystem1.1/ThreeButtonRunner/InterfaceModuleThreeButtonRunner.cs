using UnityEngine.UI;

namespace mech.input
{
    public class InterfaceModuleThreeButtonRunner : AInterfaceModule
    {
        // buttons[left, right, up]
        public InterfaceModuleThreeButtonRunner(IEventHandlerThreeButtonRunner reciever, Image[] buttons)
        {
            eventModules.Add(new EventModuleThreeButtonRunner(reciever, buttons));
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            return true;
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            return false;
        }
    }
}