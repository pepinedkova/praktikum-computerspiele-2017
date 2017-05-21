using mech.input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversityRunGame : MonoBehaviour
{
    public ControlUIGame controlUIGame;

    private InputTrackerDynamic inputTrackerDynamic;

    private bool isRunning = false;

    public void Begin()
    {
        inputTrackerDynamic = new InputTrackerDynamic();
        inputTrackerDynamic.AddModule(1, new InterfaceModuleThreeStick(controlUIGame.scriptPlayer as IEventHandlerThreeStick, controlUIGame.imagesButtons));
        inputTrackerDynamic.AddModule(0, new InterfaceModuleSnapPlate(controlUIGame.scriptPlayer as IEventHandlerSnapPlate, controlUIGame.imageSnapPlate));
        inputTrackerDynamic.SetActive(true);

        isRunning = true;
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }

    private void Update()
    {
        if(isRunning) inputTrackerDynamic.UpdateTouches(Input.touches, Time.deltaTime);
    }

    public void End()
    {

    }
}