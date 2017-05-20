using UnityEngine;

namespace mech.input
{
    public enum PhasePointer { BEGAN, MOVED, STATIC, ENDED }

    public class TouchData
    {
        public int idTouch;
        public PhasePointer phasePointer;

        // General
        public bool isRelevantForInterfaceModule = false;

        public bool isFreeMove = true;
        public bool removeAtEnd = false;

        public MonoBehaviour monoOrigin;
        public MonoBehaviour monoCurrent;
        public MonoBehaviour monoPrev;

        public Vector3 posHit;

        public Vector2 posOrigin;
        public Vector2 position;
        public Vector2 posPrev;
        public Vector2 delta;

        public float timeTouch;
        public float timeOnObject;
        public float distTouch;

        // Tap
        public bool neverMoved;

        // Hold
        public float timeStatic = 0f;
        public bool hasSentHold = false;

        // Swipe
        public float distSwipe;
        public int dirSwipe; // 0-right, 1-left, 2-up, 3-down, 4-undefined
        public Vector2 deltaSwipe;
        public int nSwipes;

        // Resize
        public float factorResize;

        // Rotate 
        public Vector2 vecStart;
        public float degreeRotate;

        //public bool isFreeForEvent;

        public TouchData()
        {
            monoOrigin = null;
            monoCurrent = null;
            monoPrev = null;
            posHit = Vector3.zero;
            posOrigin = Vector2.zero;
            position = Vector2.zero;
            posPrev = Vector2.zero;
            delta = Vector2.zero;
            timeTouch = 0f;
            timeOnObject = 0f;
            distTouch = 0f;
 //           isFreeForEvent = true;

            neverMoved = true;

            timeStatic = 0f;
            hasSentHold = false;

            nSwipes = 0;
            distSwipe = 0f;
            deltaSwipe = Vector2.zero;
            dirSwipe = 4;

            factorResize = 0f;

            vecStart = Vector2.zero;
            degreeRotate = 0f;
        }
    }
}