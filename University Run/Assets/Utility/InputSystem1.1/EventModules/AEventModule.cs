using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mech.input
{
    public abstract class AEventModule
    {
        public bool isNotifier = true;
        public IEventHandler reciever; 

        public abstract bool SearchEvent(List<TouchData> touchDatas, float tDelta);        
    
        // used for multiple modules
        protected int GetTouchesOnOrigin(List<TouchData> touchDatas)
        {
            return touchDatas.Where(i => (i.monoOrigin != null)).Count();
        }

        protected int Vec2Dir(Vector2 delta)
        {
            if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                return (delta.x > 0)? 0 : 1; // right, left
            else
                return (delta.y > 0)? 2 : 3; // up, down
        }  
    }
}