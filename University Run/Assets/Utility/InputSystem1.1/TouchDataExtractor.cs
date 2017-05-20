using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mech.input
{
    public class TouchDataExtractor
    {
        public Dictionary<int, TouchData> touchData = new Dictionary<int, TouchData>();

#if UNITY_EDITOR

        int idButtonMouse = 0;

#endif

        public void UpdateTouchData(Touch[] touches, float tDelta)
        {

#if UNITY_ANDROID
            // remove tds whose touches are already gone
            //List<int> keysTouchData = touchData.Keys.ToList();
            //List<int> idsTouches = touches.Select(i => i.fingerId).ToList();

            //foreach(int id in keysTouchData)
            //{
            //    if(!idsTouches.Contains(id))
            //    {
            //        if(!touchData.ContainsKey(id)) MobileConsole.Say("remove CANT BE :(");
            //        touchData.Remove(id);
            //    }
            //}

            foreach (Touch touch in touches)
            {
                int idTouch = touch.fingerId;

                //if(!touchData.ContainsKey(touch.fingerId))
                //{
                //    MobileConsole.Say("skip if not relevant in td");
                //    break;
                //}

                //td = touchData[touch.fingerId];

                switch(touch.phase)
                {      
                    case TouchPhase.Began:
                    {       
                        if(touchData.ContainsKey(idTouch))
                        {
                            //DevCon.Say("ERROR - contains began");
                        }
                        else
                        {
                            TouchData tdBegan = new TouchData();

                            tdBegan.phasePointer = PhasePointer.BEGAN;
                            tdBegan.idTouch = touch.fingerId;
                            tdBegan.posOrigin = touch.position;
                            tdBegan.position = touch.position;
                            tdBegan.posPrev = touch.position;

                            MonoBehaviour monoHit;
                            Vector3 posHit;
                            if(GetObjsHit(touch.position, out posHit, out monoHit))
                            {                      
                                tdBegan.monoOrigin = monoHit;
                                tdBegan.monoCurrent = monoHit;
                                tdBegan.posHit = posHit;
                            }

                            touchData.Add(touch.fingerId, tdBegan);
                        }
                                                                                
                        break;
                    }    
                    case TouchPhase.Moved:
                    {         
                        if(!touchData.ContainsKey(idTouch))
                        {
                            //DevCon.Say("ERROR - not contained move");
                        }
                        else
                        {
                            TouchData td = touchData[idTouch];
                            td.phasePointer = PhasePointer.MOVED;
                            td.timeTouch += tDelta;
                            td.timeStatic = 0f;

                            td.posPrev = td.position;
                            td.position = touch.position;
                            td.delta = td.position - td.posPrev;
                            td.deltaSwipe = td.position - td.posOrigin;
                            td.distSwipe = td.deltaSwipe.magnitude;
                            td.distTouch += td.delta.magnitude;

                            td.monoPrev = td.monoCurrent;

                            MonoBehaviour monoHit;
                            Vector3 posHit;
                            if(GetObjsHit(touch.position, out posHit, out monoHit))
                            {
                                td.monoCurrent = monoHit;
                                td.posHit = posHit;
                            }
                            else
                                td.monoCurrent = null;              

                            if(td.monoCurrent != null) td.timeOnObject += tDelta;

                            if(td.monoCurrent != td.monoPrev)
                            {
                                td.timeOnObject = 0f;
                                td.hasSentHold = false;
                            }
                        }        
                        break;
                    }
                    case TouchPhase.Stationary:
                    {
                        if(!touchData.ContainsKey(idTouch))
                        {
                            //DevCon.Say("ERROR - not contained stationary");
                        }
                        else
                        {
                            TouchData td = touchData[idTouch];
                            td.phasePointer = PhasePointer.STATIC;
                            td.timeTouch += tDelta;
                            td.timeStatic += tDelta;
                            td.timeOnObject += tDelta;
                            td.delta = Vector2.zero;
                        }
                        break;
                    } 
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                    {
                        if(!touchData.ContainsKey(idTouch))
                        {
                            //DevCon.Say("ERROR - not contained canceled, ended");
                        }
                        else
                        {
                            TouchData td = touchData[idTouch];
                            td.phasePointer = PhasePointer.ENDED;
                            td.removeAtEnd = true;
                        }
                        break;
                    }
                    default:
                    {
                        //DevCon.Say("ERROR - phase default");
                        break;
                    }
                }
            }

#endif

#if UNITY_EDITOR

            if(Input.GetMouseButtonDown(idButtonMouse))
            {
                if (touchData.ContainsKey(idButtonMouse))
                {
                    //DevCon.Say("ERROR - contains began");
                }
                else
                {
                    TouchData tdBegan = new TouchData();

                    tdBegan.idTouch = idButtonMouse;
                    tdBegan.posOrigin = Input.mousePosition;
                    tdBegan.position = Input.mousePosition;
                    tdBegan.posPrev = Input.mousePosition;

                    MonoBehaviour monoHit;
                    Vector3 posHit;
                    if (GetObjsHit(Input.mousePosition, out posHit, out monoHit))
                    {
                        tdBegan.monoOrigin = monoHit;
                        tdBegan.monoCurrent = monoHit;
                        tdBegan.posHit = posHit;
                    }

                    touchData.Add(idButtonMouse, tdBegan);
                }
            }
            else if(Input.GetMouseButton(idButtonMouse))
            {
                if (!touchData.ContainsKey(idButtonMouse))
                {

                }
                else
                {
                    TouchData td = touchData[idButtonMouse];
                    if (td.posPrev == td.position)
                    {
                        // stationary
                        td.phasePointer = PhasePointer.STATIC;
                        td.timeTouch += tDelta;
                        td.timeStatic += tDelta;
                        td.timeOnObject += tDelta;
                        td.delta = Vector2.zero;
                    }
                    else
                    {
                        // moved
                        td.phasePointer = PhasePointer.MOVED;
                        td.timeTouch += tDelta;
                        td.timeStatic = 0f;

                        td.posPrev = td.position;
                        td.position = Input.mousePosition;
                        td.delta = td.position - td.posPrev;
                        td.deltaSwipe = td.position - td.posOrigin;
                        td.distSwipe = td.deltaSwipe.magnitude;
                        td.distTouch += td.delta.magnitude;

                        td.monoPrev = td.monoCurrent;

                        MonoBehaviour monoHit;
                        Vector3 posHit;
                        if (GetObjsHit(Input.mousePosition, out posHit, out monoHit))
                        {
                            td.monoCurrent = monoHit;
                            td.posHit = posHit;
                        }
                        else
                            td.monoCurrent = null;

                        if (td.monoCurrent != null) td.timeOnObject += tDelta;

                        if (td.monoCurrent != td.monoPrev)
                        {
                            td.timeOnObject = 0f;
                            td.hasSentHold = false;
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp(idButtonMouse))
            {
                if (!touchData.ContainsKey(idButtonMouse))
                {
                    //DevCon.Say("ERROR - not contained canceled, ended");
                }
                else
                {
                    TouchData td = touchData[idButtonMouse];
                    td.phasePointer = PhasePointer.ENDED;
                    td.removeAtEnd = true;
                }
            }

#endif

        }

        public void ClearRemoveAtEnd()
        {
            foreach(TouchData td in touchData.Values.ToList())
            {
                if(td.removeAtEnd) touchData.Remove(td.idTouch);
            }
        }
        
        private bool GetObjsHit(Vector2 posTouch, out Vector3 posHit, out MonoBehaviour monoHit) // null if no obj hit or no script attached
        {
            RaycastHit rch;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(posTouch), out rch)) // first is closest
            {
                posHit = rch.point;
                monoHit = rch.collider.GetComponent<MonoBehaviour>();
                return true;           
            }
            else
            {
                posHit = Vector3.zero;
                monoHit = null;
                return false;
            }
        }
    }
}