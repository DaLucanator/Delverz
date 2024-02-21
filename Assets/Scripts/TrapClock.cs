using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapClock : MonoBehaviour
{
    public static TrapClock current;
    private bool shouldActivateOn, shouldActivateOff;

    public void Awake()
    {
        current = this;
    }

    public void Start()
    {
        InvokeRepeating("TickMethod", 1, 1);
    }

    public event Action tick;
    public event Action<bool> onTick;
    public event Action<bool> offTick;
    private void TickMethod()
    {
        shouldActivateOn = !shouldActivateOn;
        shouldActivateOff = !shouldActivateOff;
        if (tick != null) { tick(); }
        if (onTick != null) { onTick(shouldActivateOn); }
        if (offTick != null) { offTick(shouldActivateOn); }

        //doors switch state
        //spikes switch state
        //arrows shoot everytime
    }
}
