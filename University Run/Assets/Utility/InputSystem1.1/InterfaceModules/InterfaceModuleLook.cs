namespace mech.input
{
    public class InterfaceModuleLook : AInterfaceModule
    {
        public InterfaceModuleLook(IEventHandlerLook eventHandlerLook)
        {
            eventModules.Add(new EventModuleLook(eventHandlerLook));
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            // lowest module - accepts all fallen through touches
            return true;
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            // no passing needed
            return false;
        }
    }
}