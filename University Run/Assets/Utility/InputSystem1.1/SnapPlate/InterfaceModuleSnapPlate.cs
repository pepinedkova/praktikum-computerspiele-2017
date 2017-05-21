using UnityEngine.UI;

namespace mech.input
{
    public class InterfaceModuleSnapPlate : AInterfaceModule
    {
        public InterfaceModuleSnapPlate(IEventHandlerSnapPlate reciever, Image plate)
        {
            eventModules.Add(new EventModuleSnapPlate(reciever, plate));
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            // every touch is relevant, no need to narrow down
            return true;
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            return false;
        }
    }
}