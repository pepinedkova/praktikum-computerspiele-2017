namespace mech.input
{
    public class InterfaceModuleInteractionSimple : AInterfaceModule
    {
        public InterfaceModuleInteractionSimple()
        {
            eventModules.Add(new EventModulePress());
            eventModules.Add(new EventModuleRelease());
        }

        public override bool IsTouchRelevant(TouchData td)
        {
            return td.monoOrigin != null && td.monoOrigin is IEventHandler;
        }

        public override bool IsTouchFallingThrough(TouchData td, bool isRelevant)
        {
            return !(td.monoOrigin != null && td.monoOrigin is IEventHandler && !(td.monoOrigin is ITouchFallThrough));
        }
    }
}