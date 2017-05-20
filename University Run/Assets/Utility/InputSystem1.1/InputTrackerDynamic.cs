using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace mech.input
{
    public class InputTrackerDynamic
    {
        private bool isActive;

        private TouchDataExtractor touchDataExtractor;
        private List<AInterfaceModule> interfaceModules;

        public InputTrackerDynamic()
        {
            isActive = false;
            interfaceModules = new List<AInterfaceModule>();
            touchDataExtractor = new TouchDataExtractor();
        }

        public void AddModule(int idxLayer, AInterfaceModule interfaceModule)
        {
            interfaceModule.idxLayer = idxLayer;
            interfaceModules.Add(interfaceModule);
            interfaceModules = interfaceModules.OrderByDescending(o=>o.idxLayer).ToList();
            //interfaceModules = interfaceModules.OrderBy(o=>o.idxLayer).ToList();
        }

        public void SetActive(bool active)
        {
            isActive = active;
        }

        public void ClearModules()
        {
            interfaceModules.Clear();
        }

        public void UpdateTouches(Touch[] touches, float timeDelta)
        {
            if(!isActive) return;

            touchDataExtractor.UpdateTouchData(touches, timeDelta);

            List<TouchData> touchesFallThrough = new List<TouchData>(touchDataExtractor.touchData.Values);
            foreach(AInterfaceModule interfaceModule in interfaceModules)
            {
                interfaceModule.UpdateModule(touchesFallThrough, timeDelta);
                touchesFallThrough = interfaceModule.touchDataFallThrough;
            }

            foreach(AInterfaceModule interfaceModule in interfaceModules)
            {
                interfaceModule.EvaluateEvents(timeDelta);
            }

            touchDataExtractor.ClearRemoveAtEnd();       
        }
    }
}