using System.Collections.Generic;

namespace mech.input
{
    public abstract class AInterfaceModule
    {
        public int idxLayer = 0; // lower values are lower layers
        protected List<AEventModule> eventModules = new List<AEventModule>();
       
        // module can decide
        public abstract bool IsTouchRelevant(TouchData td);
        public abstract bool IsTouchFallingThrough(TouchData td, bool isRelevant);

        public List<TouchData> touchDataRelevant;
        public List<TouchData> touchDataFallThrough;

        public void UpdateModule(List<TouchData> touchDatas, float timeDelta)
        {
            touchDataRelevant = new List<TouchData>();
            touchDataFallThrough = new List<TouchData>();

            foreach(TouchData td in touchDatas)
            {
                bool isRelevant = IsTouchRelevant(td);

                if(isRelevant) touchDataRelevant.Add(td);

                if(IsTouchFallingThrough(td, isRelevant)) touchDataFallThrough.Add(td);
            }
        }
      
        public void EvaluateEvents(float tDelta)
        {
            foreach(AEventModule module in eventModules) module.SearchEvent(touchDataRelevant, tDelta);
        }
    }
}